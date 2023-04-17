
namespace WORKFLOW.Services
{
    public interface IWorkflowServices
    {
        Task<string[]> SetWorkFlow();
        Task<Response<bool>> SetupDocumentWorkflow(DocumentRequestDto data);
        Task<Response<bool>> CloseWorkflow(CloseWorkflowRequestDto data);
        Task<bool> insertDefault();
    }

    public class WorkflowServices : IWorkflowServices
    {
        private readonly IWorkflowDao _workflowDao;
        private readonly IWorkflowHelper _workflowHelper;

        public WorkflowServices(IWorkflowDao workflowDao, IWorkflowHelper workflowHelper)
        {
            _workflowDao = workflowDao;
            _workflowHelper = workflowHelper;
        }

        public async Task<string[]> SetWorkFlow()
        {
            string[] workflowRules;
            List<string> WorkflowList = new List<string>();

            List<ms_workflow> listWorkflow = await _workflowDao.getListWorkflow();

            if (listWorkflow.Count > 0) {
                List<RuleWorkflow> listRuleWorkflows = new List<RuleWorkflow>();

                foreach (var workflowHeader in listWorkflow) {
                    //Add Workflow
                    RuleWorkflow ruleWorkflow = new RuleWorkflow();

                    ruleWorkflow.WorkflowName = workflowHeader.workflowCode;

                    //get data rule table
                    List<ms_rule> listRule = await _workflowDao.getListRuleByWorkflowCode(workflowHeader.workflowCode);

                    //Add Global Params
                    if (workflowHeader.md_workflows?.Count > 0) {
                        List<glblpprm> listGlobalParams = new List<glblpprm>();

                        foreach (var loopGlobalParams in workflowHeader.md_workflows) {
                            glblpprm GlobalParams = new glblpprm();
                            GlobalParams.Name = loopGlobalParams.paramcode;
                            GlobalParams.Expression = loopGlobalParams.paramsexpression;
                            listGlobalParams.Add(GlobalParams);
                        }

                        ruleWorkflow.GlobalParams = listGlobalParams;
                    }

                    //Add Rule 
                    if (listRule.Count > 0) {
                        List<rules> listRules = new List<rules>();

                        foreach (var loopRules in listRule) {
                            string ruleExp = "";
                            int groupLine = 0;
                            int countGroupLine = 0;

                            List<lclprms> listLocalParams = new List<lclprms>();

                            //Add Local Params
                            //Add Local Params Variable
                            if (loopRules.md_rule_vars?.Count > 0) {
                                foreach (var loopLocalParamsVar in loopRules.md_rule_vars) {
                                    lclprms localParamas = new lclprms();
                                    localParamas.Name = loopLocalParamsVar.paramcode;
                                    localParamas.Expression = loopLocalParamsVar.paramsexpression;
                                    listLocalParams.Add(localParamas);
                                }
                            }

                            //Add Local Params Expresion
                            if (loopRules.md_rule_exps?.Count > 0) {
                                foreach (var loopLocalParamsExp in loopRules.md_rule_exps) {
                                    lclprms localParamas = new lclprms();
                                    localParamas.Name = loopLocalParamsExp.paramcode;
                                    localParamas.Expression = loopLocalParamsExp.paramsexpression;
                                    listLocalParams.Add(localParamas);

                                    var linkExp = loopLocalParamsExp.linkexp != null &&
                                                    loopLocalParamsExp.linkexp != "" ? " " + loopLocalParamsExp.linkexp + " " : "";

                                    if (loopLocalParamsExp.groupline != 0) {
                                        if (loopLocalParamsExp.groupline > groupLine) {
                                            countGroupLine = loopRules.md_rule_exps
                                                                .Where(q => q.groupline == loopLocalParamsExp.groupline).Count();
                                            ruleExp += "(" + loopLocalParamsExp.paramcode + linkExp;
                                            groupLine++;
                                            countGroupLine--;
                                        } else {
                                            countGroupLine--;
                                            if (countGroupLine == 0) {
                                                ruleExp += loopLocalParamsExp.paramcode + ")" + linkExp;
                                            } else {
                                                ruleExp += loopLocalParamsExp.paramcode + linkExp;
                                            }
                                        }
                                    } else {
                                        ruleExp += loopLocalParamsExp.paramcode + linkExp;
                                    }
                                }
                            }

                            //get data result 
                            ms_rule getDataResult = await _workflowDao
                                                            .getRuleByWorkflowCodeAndRulesCodeforCustomAction(
                                                                                workflowHeader.workflowCode, loopRules.rulecode);

                            context addContext = new context()
                            {
                                data = getDataResult
                            };

                            onsuccess addOnsuccess = new onsuccess()
                            {
                                Name = "ResultPromo",
                                Context = addContext
                            };

                            action addAction = new action()
                            {
                                OnSuccess = addOnsuccess
                            };

                            rules rules = new rules()
                            {
                                Actions = addAction,
                                RuleName = loopRules.rulecode,
                                SuccessEvent = "#",
                                LocalParams = listLocalParams,
                                Expression = ruleExp,
                            };

                            listRules.Add(rules);
                        }

                        ruleWorkflow.Rules = listRules;
                        WorkflowList.Add(JsonConvert.SerializeObject(ruleWorkflow));
                    }

                }

                //await _workflowDao.deleteRedislistRuleHeader();
            }

            workflowRules = WorkflowList.ToArray();

            return workflowRules;
        }

        public async Task<Response<bool>> SetupDocumentWorkflow(DocumentRequestDto data)
        {
            Response<bool> response = new Response<bool>();

            try {
                var cekDocumentWofklow = await _workflowDao.getListViewSelectedWorkflow(data.docNumber!);

                if(cekDocumentWofklow.Count < 1) {
                    var workflowResult = await _workflowHelper.GetWorkFlow(data.module, data);
                    var workflowResultSingle = workflowResult.Where(q => q.IsSuccess).FirstOrDefault();

                    if (workflowResultSingle != null) {
                        var dataResultDetailString = JsonConvert.SerializeObject(workflowResultSingle?.ActionResult.Output);
                        var dataResultDetail = JsonConvert.DeserializeObject<List<tr_workflow>>(dataResultDetailString);
                        await _workflowDao.insertTransWorkflow(dataResultDetail!);
                        await _workflowDao.closeSelfWorkflow(data.docNumber, data.creator);
                        response.Data = true;
                    } else {
                        response.Success = false;
                        response.Message = "No Have Workflow for This Document !";
                    }
                } else {
                    response.Success = false;
                    response.Message = "Document Already have Workflow !";
                }

            } catch (Exception ex) {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<bool>> CloseWorkflow(CloseWorkflowRequestDto data)
        {
            Response<bool> response = new Response<bool>();

            var DataWorkflow = await _workflowDao.getViewSelectedWorkflowNext(data.documentNumber!);
            
            if (DataWorkflow.Count > 0) {
                var GetLineGroup = DataWorkflow?.FirstOrDefault()?.linegroup;
                DataWorkflow = DataWorkflow?.Where(q => q.linegroup == GetLineGroup).ToList();
                DataWorkflow = DataWorkflow?.Where(q => q.username!.Contains(data.userName!)).ToList();

                if(DataWorkflow?.Count > 0) {
                    var cekSubmitWorkflow = await _workflowDao.closeWorkflow(data.documentNumber!, GetLineGroup, data.userName!);

                    if (cekSubmitWorkflow) {

                        var cekDataFinish = await _workflowDao.getViewSelectedWorkflowNext(data.documentNumber!);

                        if(cekDataFinish?.FirstOrDefault()?.rulecode == "FINISH") {
                            await _workflowDao.closeFinishWorkflow(data.documentNumber!, data.userName!);
                        }

                        response.Data = true;
                    } else {
                        response.Success = false;
                        response.Message = "Failed Submit Workflow !";
                    }
                } else {
                    response.Success = false;
                    response.Message = "User not Have Access to Approve or Review !";
                }
            } else {
                response.Success = false;
                response.Message = "No Have Document Workflow !";
            }

            return response;
        }

        public async Task<bool> insertDefault()
        {
            bool result;
            MyDb myDb = new MyDb();

            try {
                result = await _workflowDao.insertDefault(myDb.listmsworkflow, myDb.listmsgroupworkflow, myDb.listmsuser);
            } catch {
                result = false;
            }

            return result;
        }
    }
}
