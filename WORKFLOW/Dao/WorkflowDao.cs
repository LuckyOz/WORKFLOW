
namespace WORKFLOW.Dao
{
    public interface IWorkflowDao
    {
        Task<List<ms_workflow>> getListWorkflow();
        Task<List<ms_rule>> getListRuleByWorkflowCode(string workflowCode);
        Task<ms_rule> getRuleByWorkflowCodeAndRulesCodeforCustomAction(string workflowCode, string rulesCode);
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
                //.Include(q => q.md_rule_rsls!.OrderBy(q => q.linenum))
                .ToListAsync();
        }

        public async Task<ms_rule> getRuleByWorkflowCodeAndRulesCodeforCustomAction(string workflowCode, string rulesCode)
        {
            ms_rule? dataRule = new ms_rule();
            dataRule = await _workflowContext.ms_rules!.AsNoTracking()
                       .Where(q => q.workflowcode == workflowCode && q.rulecode == rulesCode && q.isactive && q.enddate >= DateTime.UtcNow)
                       //.Include(q => q.md_rule_rsls.OrderBy(q => q.linenum))
                       .FirstOrDefaultAsync();

            return dataRule!;
        }
    }
}
