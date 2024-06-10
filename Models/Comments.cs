using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace courseManagementSystemV1.Models
{
    public class Comments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int commentID {  get; set; }
        
        [Required]
        [StringLength(500)]
        public string commentText { get; set; }

        public string? commentphoto { get; set; }
        public DateTime? commentdate { get; set; }
        public DateTime? commentUpdate {  get; set; }

        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }

        public int CourseID { get; set; }
        [ForeignKey("CourseID")]
        public Course Course { get; set; }


        public int commentParentID { get; set; }
        [ForeignKey("commentParentID")]
        public Comments comments { get; set; }
    }
}
