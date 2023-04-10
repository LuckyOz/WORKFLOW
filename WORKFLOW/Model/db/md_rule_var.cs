
namespace WORKFLOW.Model.db
{
    public class md_rule_var
    {
        [ForeignKey("ms_promo_workflow")]
        [Required]
        [Column("workflowcode")]
        [StringLength(100)]
        public string workflowcode { get; set; } = string.Empty;

        [ForeignKey("ms_promo_rule")]
        [Required]
        [Column("rulecode")]
        [StringLength(100)]
        public string rulecode { get; set; } = string.Empty;

        [Required]
        [Column("linenum")]
        public int linenum { get; set; }

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
        [StringLength(9999999)]
        public string paramsexpression { get; set; } = string.Empty;
    }
}
