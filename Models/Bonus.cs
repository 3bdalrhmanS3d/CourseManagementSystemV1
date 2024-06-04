using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace courseManagementSystemV1.Models
{
    public class Bonus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BonusID { get; set; }

        public int StudentID { get; set; }
        [ForeignKey("StudentID")]
        public User Student { get; set; }

        public int CourseID { get; set; }
        [ForeignKey("CourseID")]
        public Course Course { get; set; }

        public string BonusType { get; set; }
        public decimal BonusAmount { get; set; }
        public DateTime DateAwarded { get; set; }
    }
}
