using courseManagementSystemV1.DBContext;
using Microsoft.AspNetCore.Mvc;
using courseManagementSystemV1.Models;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace courseManagementSystemV1.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _host;
        public RegistrationController(AppDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        public class Input
        {
            [Required]
            [StringLength(14, ErrorMessage = "Please do not enter values over 14 characters")]
            public string userSSN { get; set; }

            [Required]
            [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Username must contain only letters.")]
            public string userFirstName { get; set; }

            [Required]
            [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Username must contain only letters.")]
            public string userMiddelName { get; set; }

            [Required]
            [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Username must contain only letters.")]
            public string userLastName { get; set; }

            [Required]
            [EmailAddress]
            public string userEmail { get; set; }

            [Required]
            [StringLength(14, ErrorMessage = "Please do not enter values over 14 characters")]
            public string userPhoneNumber { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)[a-zA-Z\d]+$", ErrorMessage = "Password must contain both letters and numbers.")]
            public string userPassword { get; set; }

            [Required]
            [Compare("userPassword", ErrorMessage = "The fields Password and PasswordConfirmation should be equals")]
            public string userConfPassword { get; set; }

            [Required]
            public string userEnterCollegeDateTime { get; set; } 

            [Required]
            public string userCollege { get; set; }

            [Required]
            public string userDepartment { get; set; } = "Genral";
            [DisplayFormat(DataFormatString = "{0:MMM.DD.YYYY}")]
            public DateTime? UserBirthDay { get; set; }
            [Required]
            public string userUniversity { get; set; } = "Assiut";

            public string? userPhoto { get; set; }
            public IFormFile img_file { get; set; }

            [Required]
            public string userAddressGov { get; set; }

            public string? userCity { get; set; }
            public string? userstreet { get; set; }
        }

        [BindProperty]
        public Input input { get; set; }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.message = "Welcome";
            return View();
        }

        // POST: /Registration/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Input input)
        {
            
            if (ModelState.IsValid)
            {
                var existingUserByEmail = _context.Users.SingleOrDefault(x => x.UserEmail == input.userEmail);
                var existingUserBySSN = _context.Users.SingleOrDefault(x => x.UserSSN == input.userSSN);
                if (existingUserByEmail != null || existingUserBySSN != null )
                {
                    ViewBag.message = "User already exists";
                    return RedirectToAction("Index", "Login");
                }

                if (input.img_file != null && input.img_file.Length > 0)
                {
                    try
                    {
                        // Get the wwwroot path
                        string uploadPath = Path.Combine(_host.WebRootPath, "images");
                        Directory.CreateDirectory(uploadPath);
                        // Generate a unique filename for the uploaded file
                        string newfilename = $"{Guid.NewGuid()}{Path.GetExtension(input.img_file.FileName)}";
                        string fileName = Path.Combine(uploadPath, newfilename);

                        // Save the file to the server
                        using (var fileStream = new FileStream(fileName, FileMode.Create))
                        {
                            await input.img_file.CopyToAsync(fileStream);
                        }

                        input.userPhoto = Path.Combine("/images/", newfilename);
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions
                        return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading file: " + ex.Message);
                    }
                }

                User newuser = new User
                {
                    UserSSN = input.userSSN,
                    UserFirstName = input.userFirstName,
                    UserLastName = input.userLastName,
                    UserMiddelName = input.userMiddelName,
                    UserEmail = input.userEmail,
                    UserPhoneNumber = input.userPhoneNumber,
                    UserPassword = input.userPassword,
                    UserConfPassword = input.userConfPassword,
                    UserEnterCollegeDateTime = input.userEnterCollegeDateTime,
                    UserCollege = input.userCollege,
                    UserDepartment = input.userDepartment,
                    UserUniversity = input.userUniversity,
                    UserAddressGov = input.userAddressGov,
                    UserCity = input.userCity,
                    UserStreet = input.userstreet,
                    UserPhoto = input.userPhoto,
                    UserBirthDay = input.UserBirthDay,
                    UserCreatedAccount = DateTime.Now,
                    IsBlocked = false,
                    IsDeleted = false,
                    NormalUser = true,
                    IsUserHR = false,
                };

                _context.Users.Add(newuser);
                await _context.SaveChangesAsync();
                ViewBag.message = "Registration successful";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.message = "Registration failed";
                return View(input);
            }
        }
    }
}
