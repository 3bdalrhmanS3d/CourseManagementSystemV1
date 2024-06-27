using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace courseManagementSystemV1.Models
{
    public class QuizAnswer
    {
        /*
         The Quiz Answer Table can be used to store
        the answers of single-choice, multiple-choice and select
        type questions. In the case of a single-choice question,
        the answers can be Yes and No 
          
         */
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnswerID { get; set; }
        [ForeignKey("QuizQuestion")]
        public int QuestionID { get; set; }
        public string AnswerContent { get; set; }
        public bool IsCorrect { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public QuizQuestion QuizQuestion { get; set; }
    }
}
