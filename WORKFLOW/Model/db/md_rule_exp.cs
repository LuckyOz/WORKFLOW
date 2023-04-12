
namespace WORKFLOW.Model.db
{
    public class md_rule_exp
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

        [Required]
        [Column("linenum")]
        public int linenum { get; set; }

        [Key]
        [Required]
        [Column("groupline")]
        public int groupline { get; set; }

        [AllowNull]
        [Column("linkexp")]
        [StringLength(10)]
        public string linkexp { get; set; }

        [Key]
        [Required]
        [Column("paramcode")]
        [StringLength(100)]
        public string paramcode { get; set; } = string.Empty;

        [Required]
        [Column("paramname")]
        [StringLength(100)]
        public string paramname { get; set; } = string.Empty;

        [Required]
        [Column("paramsexpression")]
        [StringLength(5000)]
        public string paramsexpression { get; set; } = string.Empty;
    }
}
