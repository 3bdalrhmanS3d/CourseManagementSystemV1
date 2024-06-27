using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace courseManagementSystemV1.Models
{
    public class Take
    {
        // Take Table to track the enrollment and timing of user attempts to the quizzes
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TakeID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        [ForeignKey("Quiz")]
        public int QuizID { get; set; }
        public string Status { get; set; } // Enrolled, Started, Paused, Finished, Declared
        public int Score { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public string Content { get; set; }

        public User User { get; set; }
        public Quiz Quiz { get; set; }
        public ICollection<TakeAnswer> TakeAnswers { get; set; }

    }
}
