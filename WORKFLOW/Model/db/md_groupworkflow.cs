namespace WORKFLOW.Model.db
{
    [Table("md_groupworkflow")]
    public class md_groupworkflow
    {
        [ForeignKey("ms_groupworkflow")]
        [Required]
        [Column("groupworkflowcode")]
        [StringLength(100)]
        public string groupworkflowcode { get; set; } = string.Empty;

        [ForeignKey("ms_user")]
        [Required]
        [Column("username")]
        [StringLength(100)]
        public string username { get; set; } = string.Empty;

        public ms_user? ms_users { get; set; }
    }
}
