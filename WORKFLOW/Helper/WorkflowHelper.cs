
namespace WORKFLOW.Helper
{
    public interface IWorkflowHelper
    {
        bool RefreshWorkFlow(string[] workflowRules);
        Task<List<RuleResultTree>> GetWorkFlow(string workflow, object data);
    }

    public class WorkflowHelper : IWorkflowHelper
    {
        private RulesEngine.RulesEngine? _rulesEngine;

        public bool RefreshWorkFlow(string[] workflowRules)
        {
            //var reSettings = new ReSettings
            //{
            //    CustomActions = new Dictionary<string, Func<ActionBase>>{
            //                              {"ResultPromo", () => new ResultPromoHelper()}
            //                          }
            //};

            //_rulesEngine = new RulesEngine.RulesEngine(workflowRules, reSettings);

            _rulesEngine = new RulesEngine.RulesEngine(workflowRules);

            return true;
        }

        public async Task<List<RuleResultTree>> GetWorkFlow(string workflowPromo, object dataParams)
        {
            var paramsWorkflow = new RuleParameter("paramsWorkflow", dataParams);
            var resultList = await _rulesEngine!.ExecuteAllRulesAsync(workflowPromo, paramsWorkflow);

            return resultList;
        }
    }
}
