
namespace WORKFLOW.Dao
{
    public interface IWorkflowDao
    {
        Task<List<ms_workflow>> getListWorkflow();
        Task<List<ms_rule>> getListRuleByWorkflowCode(string workflowCode);
        Task<List<v_selectedworkflow>> getListViewSelectedWorkflow(string docnum);
        Task<List<v_selectedworkflow>> getViewSelectedWorkflowNext(string docnum);
        Task<ms_rule> getRuleByWorkflowCodeAndRulesCodeforCustomAction(string workflowCode, string rulesCode);
        Task<bool> closeSelfWorkflow(string documentnumber, string username);
        Task<bool> closeFinishWorkflow(string documentnumber, string username);
        Task<bool> closeWorkflow(string documentnumber, int? linegroup, string username);
        Task<bool> insertTransWorkflow(List<tr_workflow> listDataTransWorkflow);
        Task<bool> insertDefault(List<ms_workflow> dataWorkflow, List<ms_groupworkflow> dataGroupWorkflow, List<ms_user> dataUserWorkflow);
    }

    public class WorkflowDao : IWorkflowDao
    {
        private readonly WorkflowContext _workflowContext;

        public WorkflowDao(WorkflowContext workflowContext)
        {
            _workflowContext = workflowContext;
        }

        public async Task<List<ms_workflow>> getListWorkflow()
        {
            return await _workflowContext.ms_workflows!.AsNoTracking().Where(q => q.isactive)
                .Include(q => q.md_workflows)
                .ToListAsync();
        }

        public async Task<List<ms_rule>> getListRuleByWorkflowCode(string workflowCode)
        {
            return await _workflowContext.ms_rules!.AsNoTracking()
                .Where(q => q.workflowcode == workflowCode && q.isactive && q.enddate >= DateTime.UtcNow)
                .Include(q => q.md_rule_vars!.OrderBy(q => q.linenum))
                .Include(q => q.md_rule_exps!.OrderBy(q => q.linenum))
                .Include(q => q.md_rule_rslts!.OrderBy(q => q.linenum))
                .ToListAsync();
        }

        public async Task<ms_rule> getRuleByWorkflowCodeAndRulesCodeforCustomAction(string workflowCode, string rulesCode)
        {
            ms_rule? dataRule = new ms_rule();
            dataRule = await _workflowContext.ms_rules!.AsNoTracking()
                       .Where(q => q.workflowcode == workflowCode && q.rulecode == rulesCode && q.isactive && q.enddate >= DateTime.UtcNow)
                       .Include(q => q.md_rule_rslts!.OrderBy(q => q.linenum))
                       .FirstOrDefaultAsync();

            return dataRule!;
        }

        public async Task<bool> insertTransWorkflow(List<tr_workflow> listDataTransWorkflow)
        {
            _workflowContext.tr_workflows?.AddRange(listDataTransWorkflow);
            await _workflowContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> insertDefault(List<ms_workflow> listDataWorkflow, List<ms_groupworkflow> listDataGroupWorkflow, List<ms_user> listDataUserWorkflow)
        {
            string messageError;

            try {
                var listMsWorkflowDelete = await _workflowContext.ms_workflows!.AsNoTracking()
                    .Include(q => q.md_workflows)
                    .ToListAsync();

                var listMsRuleDelete = await _workflowContext.ms_rules!.AsNoTracking()
                    .Include(q => q.md_rule_vars)
                    .Include(q => q.md_rule_exps)
                    .Include(q => q.md_rule_rslts)
                    .ToListAsync();

                var listMsGroupWorkflow = await _workflowContext.ms_groupworkflows!.AsNoTracking()
                    .Include(q => q.md_groupworkflows)
                    .ToListAsync();

                var listMsuser = await _workflowContext.ms_users!.AsNoTracking()
                    .ToListAsync();

                _workflowContext.ms_workflows!.RemoveRange(listMsWorkflowDelete);
                _workflowContext.ms_rules!.RemoveRange(listMsRuleDelete);
                _workflowContext.ms_groupworkflows!.RemoveRange(listMsGroupWorkflow);
                _workflowContext.ms_users!.RemoveRange(listMsuser);

                await _workflowContext.SaveChangesAsync();
            } catch (Exception ex) {
                messageError = ex.Message;
            }

            try {
                await _workflowContext.ms_workflows!.AddRangeAsync(listDataWorkflow);
                await _workflowContext.ms_groupworkflows!.AddRangeAsync(listDataGroupWorkflow);
                await _workflowContext.ms_users!.AddRangeAsync(listDataUserWorkflow);

                await _workflowContext.SaveChangesAsync();
            } catch (Exception ex) {
                messageError = ex.Message;
                return false;
            }

            return true;
        }

        public async Task<List<v_selectedworkflow>> getListViewSelectedWorkflow(string docnum)
        {
            return await _workflowContext.v_selectedworkflows!.Where(q => q.documentnumber == docnum).ToListAsync();
        }

        public async Task<bool> closeWorkflow(string documentnumber, int? linegroup, string username)
        {
            try {
                var dataDocumetnWorkflow = await _workflowContext!.tr_workflows!.Where(q => q.documentnumber == documentnumber && q.linegroup == linegroup).ToListAsync();

                if(dataDocumetnWorkflow.Count > 0) {

                    foreach(var loopDataWorkflow in dataDocumetnWorkflow) {
                        loopDataWorkflow.closedby = username;
                        loopDataWorkflow.closeddate = DateTime.UtcNow;
                    }

                    await _workflowContext.SaveChangesAsync();

                } else {
                    return false;
                }

            } catch {
                return false;
            }

            return true;
        }

        public async Task<bool> closeSelfWorkflow(string documentnumber, string username)
        {
            try {
                var dataDocumetnWorkflow = await _workflowContext!.tr_workflows!
                                            .Where(q => q.documentnumber == documentnumber && q.rulecode == "SELF" && q.groupworkflowcode == "SELF")
                                            .ToListAsync();

                if (dataDocumetnWorkflow.Count > 0) {

                    foreach (var loopDataWorkflow in dataDocumetnWorkflow) {
                        loopDataWorkflow.closedby = username;
                        loopDataWorkflow.closeddate = DateTime.UtcNow;
                    }

                    await _workflowContext.SaveChangesAsync();

                } else {
                    return false;
                }

            } catch {
                return false;
            }

            return true;
        }

        public async Task<bool> closeFinishWorkflow(string documentnumber, string username)
        {
            try {
                var dataDocumetnWorkflow = await _workflowContext!.tr_workflows!
                                            .Where(q => q.documentnumber == documentnumber && q.rulecode == "FINISH" && q.groupworkflowcode == "FINISH")
                                            .ToListAsync();

                if (dataDocumetnWorkflow.Count > 0) {

                    foreach (var loopDataWorkflow in dataDocumetnWorkflow) {
                        loopDataWorkflow.closedby = username;
                        loopDataWorkflow.closeddate = DateTime.UtcNow;
                    }

                    await _workflowContext.SaveChangesAsync();

                } else {
                    return false;
                }

            } catch {
                return false;
            }

            return true;
        }

        public async Task<List<v_selectedworkflow>> getViewSelectedWorkflowNext(string docnum)
        {
            var DataWorkflow = await _workflowContext!.v_selectedworkflows!
                                        .OrderBy(q => q.linegroup)
                                        .Where(q => q.documentnumber == docnum && (q.closedby == "" || q.closedby == null) && q.closeddate == null)
                                        .ToListAsync();

            return DataWorkflow;
        }
    }
}
