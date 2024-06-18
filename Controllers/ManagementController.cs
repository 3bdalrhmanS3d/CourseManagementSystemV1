using courseManagementSystemV1.DBContext;
using courseManagementSystemV1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Text.Json;
using System;

namespace courseManagementSystemV1.Controllers
{
    public class ManagementController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _host;
        public ManagementController(AppDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        // إضافة بيانات الـ instructor.
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult BeInstructor()
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "Mentor")
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BeInstructor(Instructor user)
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "Mentor")
            {
                return RedirectToAction("Index", "Login");
            }

            var currentUser = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("CurrentLoginUser"));

            if (ModelState.IsValid)
            {
                Instructor instructor = new Instructor
                {
                    UserID = currentUser.UserID,
                    LindenInAccount = user.LindenInAccount,
                    GitHibAccount = user.GitHibAccount,
                    instructorDateAt = DateTime.Now,
                    IsAccepted = false,
                    IsBlocked = false,
                    ISDeleted = false
                };

                _context.Instructors.Add(instructor);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "UserHome");
            }

            return View(user);
        }
    


    public class Input
        {
            public string CourseName { get; set; }
            public string CourseDescription { get; set; }
            public float CourseTime { get; set; }

            public string CourseRequirements { get; set; } = "None";
            public DateTime CourseStartDate { get; set; }
            public DateTime? CourseEndDate { get; set; }

            public string? CourseState { get; set; } = null;
            public string? CoursePhoto { get; set; }
            public IFormFile img_file { get; set; }
        }

        [BindProperty]
        public Input input { get; set; }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCourse(Course course)
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "Instructor")
            {
                return RedirectToAction("Index", "Login");
            }

            var currentUser = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("CurrentLoginUser"));
            var instructor = _context.Instructors.FirstOrDefault(i => i.UserID == currentUser.UserID && i.IsAccepted == true);

            if (instructor == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (ModelState.IsValid) 
            {

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
                        input.CoursePhoto = Path.Combine("/images/", newfilename);
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions
                        return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading file: " + ex.Message);
                    }
                }

                Course newCourse = new Course
                {
                    CourseDescription = course.CourseDescription,
                    CourseStartDate = course.CourseStartDate,
                    CourseEndDate = course.CourseEndDate,
                    CourseRequirements = course.CourseRequirements,
                    CoursePhoto = course.CoursePhoto,
                    CourseName = course.CourseName,
                    CourseState = input.CourseState == "0" ? "online" : "offline"
                };

                _context.Courses.Add(newCourse);
                await _context.SaveChangesAsync();

                CourseManagement courseManagement = new CourseManagement
                {
                    
                    courseID = course.CourseID,
                    instructorID = instructor.instructorID,
                    DateTime = DateTime.Now,
                };

                _context.courseManagements.Add(courseManagement);
                await _context.SaveChangesAsync();

                ViewBag.message = "Course successful";

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
