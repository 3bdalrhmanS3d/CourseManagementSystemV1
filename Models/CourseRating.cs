using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace courseManagementSystemV1.Models

{
    public class CourseRating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseRatingID { get; set; }
        public DateTime CourseRatingDateAt { get; set; } = DateTime.Now;

        [Required]
        [Range(0, 5)]
        public float Rating { get; set; }

        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }

        public int CourseID { get; set; }
        [ForeignKey("CourseID")]
        public Course Course { get; set; }

        [StringLength(500)]
        public string Comment { get; set; } // Optional field for additional comments

    }
}
