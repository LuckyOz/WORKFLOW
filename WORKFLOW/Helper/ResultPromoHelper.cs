﻿
namespace WORKFLOW.Helper
{
    public class ResultPromoHelper : ActionBase
    {
        public override async ValueTask<object> Run(ActionContext context, RuleParameter[] ruleParameters)
        {
            //Get Data Cart
            DocumentRequestDto? dataDocument = JsonConvert.DeserializeObject<DocumentRequestDto>(
                                        JsonConvert.SerializeObject(
                                            ruleParameters.Where(q => q.Name == "paramsWorkflow").Select(q => q.Value).FirstOrDefault()
                                            )
                                        );

            //Get Data Promo Result
            var dataWorkflow = JsonConvert.DeserializeObject<ms_rule>(context.GetContext<string>("data"));

            //Return Calculate per Promo
            return await setupWorkflow(dataWorkflow!, dataDocument!);
        }

        public async Task<List<tr_workflow>> setupWorkflow(ms_rule dataWorkflow, DocumentRequestDto dataDocument)
        {
            List<tr_workflow> response = new List<tr_workflow>();
            List<md_rule_rslt>? md_Rule_Rslts = dataWorkflow.md_rule_rslts!.OrderBy(q => q.linegroup).ToList();

            await Task.Run(() =>
            {
                int countWf = 0; 
                foreach (var loopingWorkflow in md_Rule_Rslts) {
                    tr_workflow dataTransWorkflow = new tr_workflow();

                    dataTransWorkflow.documentnumber = dataDocument.docNumber;
                    dataTransWorkflow.linegroup = loopingWorkflow.linegroup;
                    dataTransWorkflow.workflowcode = loopingWorkflow.workflowcode;
                    dataTransWorkflow.rulecode = loopingWorkflow.rulecode;
                    dataTransWorkflow.groupworkflowcode = loopingWorkflow.groupworkflowcode;
                    dataTransWorkflow.actworkflow = loopingWorkflow.actworkflow;
                    dataTransWorkflow.descworkflow = loopingWorkflow.descworkflow;

                    countWf++;

                    response.Add(dataTransWorkflow);
                }

                for(int i = 0; i < 2; i++) {
                    tr_workflow dataTransWorkflow = new tr_workflow();

                    dataTransWorkflow.documentnumber = dataDocument.docNumber;
                    dataTransWorkflow.workflowcode = md_Rule_Rslts!.FirstOrDefault()!.workflowcode;

                    if (i == 0) {
                        dataTransWorkflow.linegroup = i;
                        dataTransWorkflow.rulecode = "SELF";
                        dataTransWorkflow.groupworkflowcode = "SELF";
                        dataTransWorkflow.actworkflow = "CREATE";
                        dataTransWorkflow.descworkflow = "DOCUMENT CREATOR";
                    } else {
                        dataTransWorkflow.linegroup = countWf+1;
                        dataTransWorkflow.rulecode = "FINISH";
                        dataTransWorkflow.groupworkflowcode = "FINISH";
                        dataTransWorkflow.actworkflow = "FINISH";
                        dataTransWorkflow.descworkflow = "DOCUMENT FINISH";
                    }

                    response.Add(dataTransWorkflow);
                }
            });

            return response;
        }
    }
}
