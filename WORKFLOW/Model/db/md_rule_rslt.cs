namespace WORKFLOW.Model.db
{
    [Table("md_rule_rslt")]
    public class md_rule_rslt
    {
        [ForeignKey("ms_workflow")]
        [Required]
        [Column("workflowcode")]
        [StringLength(100)]
        public string workflowcode { get; set; } = string.Empty;

        [ForeignKey("ms_rule")]
        [Required]
        [Column("rulecode")]
        [StringLength(100)]
        public string rulecode { get; set; } = string.Empty;

        [Key]
        [Required]
        [Column("linenum")]
        public int linenum { get; set; }

        [Required]
        [Column("linegroup")]
        public int linegroup { get; set; }

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
    }
}
