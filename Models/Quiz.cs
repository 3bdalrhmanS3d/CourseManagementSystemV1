using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace courseManagementSystemV1.Models
{
    public class Quiz
    {
        /*
         we will design the Quiz Table to store the quiz data
        
         */
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuizID { get; set; }
        [ForeignKey("User")]
        public int HostID { get; set; } // instructor 
        [Required]
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        [Required]
        public string Slug { get; set; }
        public string Summary { get; set; }
        [Required]
        public string Type { get; set; } // Test or Quiz
        public int TotalScore { get; set; }
        public bool IsPublished { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
        public string Content { get; set; }

        public User Host { get; set; }
        public ICollection<QuizQuestion> Questions { get; set; }
        public ICollection<Take> Takes { get; set; }
        public ICollection<Grade> grades { get; set; }
    }
}
