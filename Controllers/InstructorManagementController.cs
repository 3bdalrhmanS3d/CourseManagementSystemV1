using courseManagementSystemV1.DBContext;
using courseManagementSystemV1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace courseManagementSystemV1.Controllers
{
    public class InstructorManagementController : Controller
    {
        private readonly AppDbContext _context;
        
        private readonly IWebHostEnvironment _host;
        public InstructorManagementController(AppDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        [HttpGet]
        public async Task <IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Login") != null )
            {
                return RedirectToAction("Index", "Login");
            }

            var jsonString = HttpContext.Session.GetString("CurrentLoginUser");
            if (string.IsNullOrEmpty(jsonString))
            {
                // Handle the case where the session variable is null or empty
                return RedirectToAction("Index", "Login");
            }

            // تحليل JSON للحصول على المستخدم الحالي
            var currentUser = JsonSerializer.Deserialize<User>(jsonString, JsonOptions.DefaultOptions);

            // استعلام للحصول على معرف المدرب بناءً على معرف المستخدم
            var instructor = await _context.Instructors
                                           .FirstOrDefaultAsync(i => i.UserID == currentUser.UserID);

            if (instructor == null)
            {
                // إذا لم يتم العثور على المدرب، التعامل مع الحالة
                return NotFound();
            }


            // استعلام للحصول على الكورسات التي أنشأها المدرب الحالي
            var myCourses = await _context.courseManagements
                                           .Include(cm => cm.course)
                                           .Where(cm => cm.instructorID == instructor.instructorID)
                                           .Select(cm => cm.course)
                                           .Distinct() // التأكد من عدم تكرار الكورسات في القائمة
                                           .ToListAsync();

            if (myCourses == null || myCourses.Count == 0)
            {
                ViewBag.Courses = null;
                return View();
            }

            ViewBag.Courses = myCourses;
            return View(myCourses);
        }
        public class Input
        {
            public List<CourseRequirement>? Requirements { get; set; }
        }

        [HttpGet]
        public async Task<IActionResult> CourseDetails(int id)
        {
            if (HttpContext.Session.GetString("Login") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var course = await _context.Courses
                                       .Include(c => c.CourseManagements)
                                           .ThenInclude(cm => cm.instructor)
                                               .ThenInclude(i => i.User)
                                       .Include(c => c.courseRatings)
                                       .Include(c => c.Comments)
                                           .ThenInclude(c => c.User) // لتضمين معلومات المستخدم الذي قام بكتابة التعليق
                                       .Include(c => c.courseSpecificQuestions)
                                       .Include(c => c.Enrollments)
                                       .FirstOrDefaultAsync(c => c.CourseID == id);

            if (course == null)
            {
                return NotFound();
            }

            ViewBag.Course = course;

            // تحميل التعليقات وتصفية التعليقات المخفية
            var comments = await _context.comments
                                         .Include(c => c.User)
                                         .Where(c => c.CourseID == id && c.hideComment == false)
                                         .OrderByDescending(c => c.commentdate)
                                         .ToListAsync();

            ViewBag.Comments = comments;
            return View(course);
        }



        [HttpGet]
        public IActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCourse(Course course, [FromForm] IFormFile img_file, List<CourseRequirement> Requirements)
        {
            if (ModelState.IsValid)
            {

                var jsonString = HttpContext.Session.GetString("CurrentLoginUser");
                var userId = JsonSerializer.Deserialize<User>(jsonString, JsonOptions.DefaultOptions);

                var instructor = await _context.Instructors.FirstOrDefaultAsync(i => i.UserID == userId.UserID);
                if (instructor == null)
                {
                    // Handle case where the instructor is not found
                    return NotFound();
                }

                string imagePath = "";

                if (img_file != null && img_file.Length > 0)
                {
                    try
                    {
                        string uploadPath = Path.Combine(_host.WebRootPath, "CourseImages");
                        Directory.CreateDirectory(uploadPath);

                        string newFilename = $"{Guid.NewGuid()}{Path.GetExtension(img_file.FileName)}";
                        string filePath = Path.Combine(uploadPath, newFilename);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await img_file.CopyToAsync(fileStream);
                        }

                        imagePath = $"/CourseImages/{newFilename}";
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, $"Error uploading file: {ex.Message}");
                    }
                }

                Course newCourse = new Course()
                {
                    CourseName = course.CourseName,
                    CourseDescription = course.CourseDescription,
                    CoursePhoto = imagePath,
                    CourseStartDate = course.CourseStartDate,
                    CourseEndDate = course.CourseEndDate,
                    IsAvailable = false, 
                    CourseTime = course.CourseTime,
                    CourseState = course.CourseState,
                };

                _context.Courses.Add(newCourse);
                await _context.SaveChangesAsync();

                // Add course requirements
                if (Requirements != null && Requirements.Count > 0)
                {
                    foreach (var requirement in Requirements)
                    {
                        if (!string.IsNullOrWhiteSpace(requirement.RequirementDescription))
                        {
                            var newRequirement = new CourseRequirement
                            {
                                RequirementDescription = requirement.RequirementDescription,
                                CourseID = newCourse.CourseID
                            };
                            _context.courseRequirements.Add(newRequirement);
                        }
                    }
                    await _context.SaveChangesAsync();
                }

                var courseManagement = new CourseManagement
                {
                    courseID = newCourse.CourseID,
                    instructorID = instructor.instructorID,
                    Isaccepted = true,
                    DateTime = DateTime.Now,
                };

                _context.courseManagements.Add(courseManagement);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(course);
        }

    }

}
