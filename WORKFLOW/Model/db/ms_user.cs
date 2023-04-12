namespace WORKFLOW.Model.db
{
    [Table("ms_user")]
    public class ms_user
    {
        [Key]
        [Required]
        [Column("username")]
        [StringLength(100)]
        public string username { get; set; } = string.Empty;

        [Required]
        [Column("name")]
        [StringLength(200)]
        public string name { get; set; } = string.Empty;

        [Required]
        [Column("email")]
        [StringLength(200)]
        public string email { get; set; } = string.Empty;

        [Required]
        [Column("isactive")]
        public bool isactive { get; set; }

        [Required]
        [Column("createdate")]
        public DateTime createdate { get; set; }

        [AllowNull]
        [Column("updatedate")]
        public System.Nullable<DateTime> updatedate { get; set; }
    }
}
