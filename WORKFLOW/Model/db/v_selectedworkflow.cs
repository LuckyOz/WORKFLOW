namespace WORKFLOW.Model.db
{
    public class v_selectedworkflow
    {
        public string? documentnumber { get; set; }
        public int linegroup { get; set; }
        public string? workflowcode { get; set; }
        public string? rulecode { get; set; }
        public string? groupworkflowcode { get; set; }
        public string? groupworkflowname { get; set; }
        public string? actworkflow { get; set; }
        public string? descworkflow { get; set; }
        public string? username { get; set; }
        public string? closedby { get; set; }
        public System.Nullable<DateTime> closeddate { get; set; }
    }
}
