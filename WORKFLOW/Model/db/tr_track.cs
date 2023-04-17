namespace WORKFLOW.Model.db
{
    public class tr_track
    {
        [Key]
        [Required]
        [Column("documentnumber")]
        [StringLength(100)]
        public string? documentnumber { get; set; }
    }
}
