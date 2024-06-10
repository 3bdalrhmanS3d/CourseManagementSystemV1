using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace courseManagementSystemV1.Models
{
    public class Instructor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int instructorID { get; set; }
        [Required]
        [StringLength(14, ErrorMessage = "Please do not enter values over 14 characters")]
        public string instructorSSN { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Username must contain only letters.")]
        public string instructorFirstName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Username must contain only letters.")]
        public string instructorMiddelName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Username must contain only letters.")]
        public string instructorLastName { get; set; }
        [Required]
        [EmailAddress]
        public string instructorEmail { get; set; }
        [Required]
        public string instructorPhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)[a-zA-Z\d]+$", ErrorMessage = "Password must contain both letters and numbers.")]

        public string instructorPassword { get; set; }
        [Required]
        [Compare("Instructor.instructorPassword", ErrorMessage = "The fields Password and PasswordConfirmation should be equals")]
        public string instructorConfPassword { get; set; }
        public string? instructorPhoto { get; set; }
        [Url]
        public string? LindenInAccount { get; set; }
        [Url]
        public string? GitHibAccount { get; set; }

        // منتور ولا لا 
        

        [DisplayFormat(DataFormatString = "{0:MMM.DD.YYYY}")]
        public DateTime instructorCreatedAccount { get; set; } = DateTime.Now;
        public bool? IsBlocked { get; set; } = false;
        public bool? ISDeleted { get; set; } = false;


        public ICollection<CourseManagement>? courseManagements { get; set; }
    }
}
