namespace WORKFLOW.Model.db
{
    [Table("ms_groupworkflow")]
    public class ms_groupworkflow
    {
        [Key]
        [Required]
        [Column("groupworkflowcode")]
        [StringLength(100)]
        public string groupworkflowcode { get; set; } = string.Empty;

        [Required]
        [Column("groupworkflowname")]
        [StringLength(200)]
        public string groupworkflowname { get; set; } = string.Empty;

        [Required]
        [Column("isactive")]
        public bool isactive { get; set; }

        [Required]
        [Column("createdate")]
        public DateTime createdate { get; set; }

        [AllowNull]
        [Column("updatedate")]
        public System.Nullable<DateTime> updatedate { get; set; }

        public List<md_groupworkflow>? md_groupworkflows { get; set; }
    }
}
