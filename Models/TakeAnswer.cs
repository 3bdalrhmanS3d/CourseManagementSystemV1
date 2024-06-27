using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace courseManagementSystemV1.Models
{
    public class TakeAnswer
    {
        /* 
         Take Answer Table can be used to 
        store the answers selected by the user while taking the quiz.
        In the case of a multiple-choice question,
        there can be multiple answers */

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TakeAnswerID { get; set; }
        [ForeignKey("Take")]
        public int TakeID { get; set; }
        [ForeignKey("QuizAnswer")]
        public int AnswerID { get; set; }
        [ForeignKey("QuizQuestion")]
        public int QuestionID { get; set; }
        public bool IsActive { get; set; } = true;
        public string Content { get; set; } // For Writing type answers

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public Take Take { get; set; }
        public QuizAnswer QuizAnswer { get; set; }
        public QuizQuestion QuizQuestion { get; set; }
    }
}
