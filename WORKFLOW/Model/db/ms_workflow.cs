
namespace WORKFLOW.Model.db
{
    [Table("ms_workflow")]
    public class ms_workflow
    {
        [Key]
        [Required]
        [Column("workflowcode")]
        [StringLength(100)]
        public string workflowCode { get; set; } = string.Empty;

        [Required]
        [Column("workflowname")]
        [StringLength(200)]
        public string workflowname { get; set; } = string.Empty;

        [Required]
        [Column("isactive")]
        public bool isactive { get; set; }

        [Required]
        [Column("createdate")]
        public DateTime createdate { get; set; }

        [AllowNull]
        [Column("updatedate")]
        public System.Nullable<DateTime> updatedate { get; set; }

        public List<md_workflow>? md_workflows { get; set; }

        public List<ms_rule>? ms_rules { get; set; }
    }
}
