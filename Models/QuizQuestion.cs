using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Humanizer;
using static System.Formats.Asn1.AsnWriter;

namespace courseManagementSystemV1.Models
{
    public class QuizQuestion
    {
        /*
        The Quiz Question Table can be used 
        to store the questions related to tests and quizzes
        */
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionID { get; set; }
        [ForeignKey("Quiz")]
        public int QuizID { get; set; }
        public Quiz quiz { get; set; }
        [Required]
        public string questionType { get; set; } = "MCQ";
        [Required]
        public string ?Content { get; set; }

        public string? Questionphoto { get; set; }
        [Required]
        public bool IsAxtive { get; set; } = false;
        public int Score { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
