using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace courseManagementSystemV1.Models
{
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeedbackId { get; set; }

        [Required]
        [StringLength(250)]
        public string FeedbackText { get; set; }

        [Required]
        public DateTime FeedbackDate { get; set; } = DateTime.Now;

        [Required]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [Required]
        public int CourseID { get; set; } // Assuming feedback is for a specific course

        [ForeignKey("CourseID")]
        public Course Course { get; set; }
    }
}
