using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace courseManagementSystemV1.Models
{
    public class Attendance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttendanceID { get; set; }

        public int EnrollmentID { get; set; }
        [ForeignKey("EnrollmentID")]
        public Enrollment Enrollment { get; set; }

        public DateTime SessionDate { get; set; }
        public TimeSpan CheckInTime { get; set; }
        public TimeSpan? CheckOutTime { get; set; }
        public TimeSpan? BreakStartTime { get; set; }
        public TimeSpan? BreakEndTime { get; set; }

        [NotMapped]
        public TimeSpan? TotalTime
        {
            get
            {
                if (CheckOutTime.HasValue && CheckInTime != null && BreakEndTime.HasValue && BreakStartTime.HasValue)
                {
                    return (CheckOutTime.Value - CheckInTime) - (BreakEndTime.Value - BreakStartTime.Value);
                }
                return null;
            }
        }
    }
}
