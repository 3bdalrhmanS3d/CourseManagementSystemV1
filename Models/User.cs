using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace courseManagementSystemV1.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        [Required]
        [StringLength(14, ErrorMessage = "Please do not enter values over 14 characters")]
        public string UserSSN { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Username must contain only letters.")]
        public string UserFirstName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Username must contain only letters.")]
        public string UserMiddelName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Username must contain only letters.")]
        public string UserLastName { get; set; }
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }
        [Required]
        [StringLength(14, ErrorMessage = "Please do not enter values over 14 characters")]
        public string UserPhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)[a-zA-Z\d]+$", ErrorMessage = "Password must contain both letters and numbers.")]
        public string UserPassword { get; set; }
        [Required]
        [Compare("UserPassword", ErrorMessage = "The fields Password and PasswordConfirmation should be equals")]
        public string UserConfPassword { get; set; }
        [Required]
        //DisplayFormat(DataFormatString = "{0:YYYY}")]

        public string UserEnterCollegeDateTime { get; set; }
        [Required]
        public string UserCollege { get; set; }

        [Required]
        public string UserDepartment { get; set; } = "Genral";
        [Required]
        public string UserUniversity { get; set; } = "Assiut";

        public string? UserPhoto { get; set; }
        [Required]
        public string UserAddressGov { get; set; }
        public string? UserCity { get; set; }
        public string? UserStreet { get; set; }
        [DisplayFormat(DataFormatString = "{0:MMM.DD.YYYY}")]
        public DateTime UserCreatedAccount { get; set; } = DateTime.Now;
        public DateTime? UserlastVisit { get; set; } = DateTime.Now;
        //Accepted
        public bool? IsAccepted { get; set; } = false;
        public DateTime? userAcceptedDate { get; set; } 
        public string ? whoAcceptedUser { get; set; }

        // rejected
        public bool? IsRejected { get; set; } = false;
        public DateTime? userRejectedDate { get; set; }
        public string? whoRejectedUser { get; set; }
        // Bolck
        public bool? IsBlocked { get; set; } = false;
        public DateTime? userBlockedDate { get; set; }
        public string? whoBlockedUser { get; set; }

        // Delete
        public bool? IsDeleted { get; set; } = false;
        public DateTime? userDeletedDate { get; set; }
        public string? whoDeletedUser { get; set; }


        public bool NormalUser { get; set; } = true;
        /// <summary>
        /// mentor ---> instructor
        /// </summary>
        public bool? IsMentor { get; set; } = false;
        public DateTime? userbeMentorDate { get; set; }
        public string? whoMentorUser { get; set; }


        public bool? IsUserHR { get; set; } = false;
        public string ? whoHrThisUser { get; set; }


        public bool? IsAdmin { get; set; } = false;
        public string? whoAdminThisUser { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<Bonus>? Bonuses { get; set; }
        public ICollection<HRManagement>? hRManagements { get; set; }
        public ICollection<VisitHistory>? visitHistories { get; set; }
        public ICollection<CourseRating>? courseRatings { get; set; }
        public ICollection<Feedback>? feedbacks { get; set; }

        public ICollection<Instructor> instructors { get; set; }
        public ICollection<Comments>? Comments { get; set; }
        public ICollection<CourseSpecificQuestions> courseSpecificQuestions { get; set; }

    }
}
