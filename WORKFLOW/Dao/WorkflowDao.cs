
namespace WORKFLOW.Dao
{
    public interface IWorkflowDao
    {
        Task<List<ms_workflow>> getListWorkflow();
        Task<List<ms_rule>> getListRuleByWorkflowCode(string workflowCode);
        Task<ms_rule> getRuleByWorkflowCodeAndRulesCodeforCustomAction(string workflowCode, string rulesCode);
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

        public async Task<bool> insertDefault(List<ms_workflow> listDataWorkflow, List<ms_groupworkflow> listDataGroupWorkflow, List<ms_user> listDataUserWorkflow)
        {
            try {
                List<string> listWFTesting = new List<string>() {
                    new string("PR")
                };

                var listMsWorkflowDelete = await _workflowContext.ms_workflows!.AsNoTracking()
                    .Where(q => listWFTesting.Contains(q.workflowCode))
                    .Include(q => q.md_workflows)
                    .ToListAsync();

                var listMsRuleDelete = await _workflowContext.ms_rules!.AsNoTracking()
                    .Where(q => listWFTesting.Contains(q.workflowcode))
                    .Include(q => q.md_rule_vars)
                    .Include(q => q.md_rule_exps)
                    .Include(q => q.md_rule_rslts)
                    .ToListAsync();

                var listMsGroupWorkflow = await _workflowContext.ms_groupworkflows!.AsNoTracking()
                    .Where(q => listWFTesting.Contains(q.workflowcode))
                    .Include(q => q.md_groupworkflows)
                    .ToListAsync();

                var listMsuser = await _workflowContext.ms_users!.AsNoTracking()
                    .ToListAsync();

                _workflowContext.ms_workflows!.RemoveRange(listMsWorkflowDelete);
                _workflowContext.ms_rules!.RemoveRange(listMsRuleDelete);
                _workflowContext.ms_groupworkflows!.RemoveRange(listMsGroupWorkflow);
                _workflowContext.ms_users!.RemoveRange(listMsuser);

                await _workflowContext.SaveChangesAsync();
            } catch (Exception ex) { }

            try {
                await _workflowContext.ms_workflows!.AddRangeAsync(listDataWorkflow);
                await _workflowContext.ms_groupworkflows!.AddRangeAsync(listDataGroupWorkflow);
                await _workflowContext.ms_users!.AddRangeAsync(listDataUserWorkflow);

                await _workflowContext.SaveChangesAsync();
            } catch (Exception ex) {
                return false;
            }

            return true;
        }
    }
}
