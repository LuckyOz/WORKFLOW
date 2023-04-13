namespace WORKFLOW.Model.db
{
    [Table("tr_workflow")]
    public class tr_workflow
    {
        [Key]
        [Required]
        [Column("documentnumber")]
        [StringLength(100)]
        public string documentnumber { get; set; } = string.Empty;

        [Key]
        [Required]
        [Column("linegroup")]
        public int linegroup { get; set; }

        [Key]
        [Required]
        [Column("workflowcode")]
        [StringLength(100)]
        public string workflowcode { get; set; } = string.Empty;

        [Key]
        [Required]
        [Column("rulecode")]
        [StringLength(100)]
        public string rulecode { get; set; } = string.Empty;

        [Key]
        [Required]
        [Column("groupworkflowcode")]
        [StringLength(100)]
        public string groupworkflowcode { get; set; } = string.Empty;

        [Required]
        [Column("actworkflow")]
        [StringLength(200)]
        public string actworkflow { get; set; } = string.Empty;

        [Column("descworkflow")]
        [StringLength(200)]
        public string descworkflow { get; set; } = string.Empty;

        [Column("closedby")]
        [StringLength(100)]
        public string closedby { get; set; } = string.Empty;

        [Column("closeddate")]
        public System.Nullable<DateTime> closeddate { get; set; }
    }
}
