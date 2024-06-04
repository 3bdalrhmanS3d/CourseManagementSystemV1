using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace courseManagementSystemV1.Models
{
    public class HRManagement
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HRManagementID { get; set; }

        [Required]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [Required]
        public int CourseID { get; set; }

        [ForeignKey("CourseID")]
        public Course Course { get; set; }

        public DateTime AssignedDate { get; set; } = DateTime.Now;


    }

}
