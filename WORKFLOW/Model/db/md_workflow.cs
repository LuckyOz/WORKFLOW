
namespace WORKFLOW.Model.db
{
    [Table("md_workflow")]
    public class md_workflow
    {
        [ForeignKey("ms_promo_workflow")]
        [Required]
        [Column("workflowcode")]
        [StringLength(100)]
        public string workflowcode { get; set; } = string.Empty;

        [Key]
        [Required]
        [Column("paramcode")]
        [StringLength(100)]
        public string paramcode { get; set; } = string.Empty;

        [Required]
        [Column("paramname")]
        [StringLength(200)]
        public string paramname { get; set; } = string.Empty;

        [Required]
        [Column("paramsexpression")]
        [StringLength(5000)]
        public string paramsexpression { get; set; } = string.Empty;
    }
}
