
namespace WORKFLOW.Model.db
{
    [Table("ms_rule")]
    public class ms_rule
    {
        [ForeignKey("ms_promo_workflow")]
        [Required]
        [Column("workflowcode")]
        [StringLength(100)]
        public string workflowcode { get; set; } = string.Empty;

        [Key]
        [Required]
        [Column("rulecode")]
        [StringLength(100)]
        public string rulecode { get; set; } = string.Empty;

        [Required]
        [Column("rulename")]
        [StringLength(200)]
        public string rulename { get; set; } = string.Empty;

        [Required]
        [Column("startdate")]
        public DateTime startdate { get; set; }

        [Required]
        [Column("enddate")]
        public DateTime enddate { get; set; }

        [Required]
        [Column("isactive")]
        public bool isactive { get; set; }

        [Required]
        [Column("createdate")]
        public DateTime createdate { get; set; }

        [AllowNull]
        [Column("updatedate")]
        public System.Nullable<DateTime> updatedate { get; set; }

        public List<md_rule_var>? md_rule_vars { get; set; }

        public List<md_rule_exp>? md_rule_exps { get; set; }

        //public List<md_promo_rule_rsl> md_promo_rule_rsls { get; set; }
    }
}
