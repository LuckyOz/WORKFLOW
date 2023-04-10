namespace WORKFLOW.Model.db
{
    [Table("md_groupworkflow")]
    public class md_groupworkflow
    {
        [Key]
        [Required]
        [Column("groupworkflowcode")]
        [StringLength(100)]
        public string groupworkflowcode { get; set; } = string.Empty;

        [Key]
        [Required]
        [Column("username")]
        [StringLength(100)]
        public string username { get; set; } = string.Empty;
    }
}
