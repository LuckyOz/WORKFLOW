namespace WORKFLOW.Model.dto
{
    public class RuleWorkflow
    {
        public string WorkflowName { get; set; } = string.Empty;
        public List<glblpprm>? GlobalParams { get; set; }
        public List<rules>? Rules { get; set; }
    }

    public class glblpprm
    {
        public string Name { get; set; } = string.Empty;
        public string Expression { get; set; } = string.Empty;
    }

    public class rules
    {
        public string RuleName { get; set; } = string.Empty;
        public string Expression { get; set; } = string.Empty;
        public string SuccessEvent { get; set; } = string.Empty;
        public List<lclprms>? LocalParams { get; set; }
        public action? Actions { get; set; }
    }

    public class lclprms
    {
        public string Name { get; set; } = string.Empty;
        public string Expression { get; set; } = string.Empty;
    }

    public class action
    {
        public onsuccess? OnSuccess { get; set; }
    }

    public class onsuccess
    {
        public string Name { get; set; } = string.Empty;
        public context? Context { get; set; }
    }

    public class context
    {
        public ms_rule? data { get; set; }
    }
}
