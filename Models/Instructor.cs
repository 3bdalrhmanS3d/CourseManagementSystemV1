using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace courseManagementSystemV1.Models
{
    public class Instructor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int instructorID { get; set; }
        
        [Url]
        public string? LindenInAccount { get; set; }
        [Url]
        public string? GitHibAccount { get; set; }

        [Required]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        // منتور ولا لا 
        public bool ? IsInstructor { get; set; } = false;

        [DisplayFormat(DataFormatString = "{0:MMM.DD.YYYY}")]
        public DateTime instructorDateAt { get; set; } = DateTime.Now;



        public bool? IsAccepted { get; set; } = false;
        public bool? IsBlocked { get; set; } = false;
        public bool? ISDeleted { get; set; } = false;

        public ICollection<CourseManagement>? courseManagements { get; set; }
    }
}
