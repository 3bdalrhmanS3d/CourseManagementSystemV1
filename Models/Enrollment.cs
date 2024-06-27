using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace courseManagementSystemV1.Models
{
    public class Enrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnrollmentID { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public bool? canAccess { get; set; } = true;
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }

        public int CourseID { get; set; }
        [ForeignKey("CourseID")]
        public Course Course { get; set; }

        public ICollection<Attendance>? Attendances { get; set; }
    }
}
