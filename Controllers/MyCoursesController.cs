using Microsoft.AspNetCore.Mvc;
using courseManagementSystemV1.Models;
using courseManagementSystemV1.DBContext;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace courseManagementSystemV1.Controllers
{
    public class MyCoursesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _host;
        public MyCoursesController(AppDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            if (HttpContext.Session.GetString("Login") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var jsonString = HttpContext.Session.GetString("CurrentLoginUser");
            var userId = JsonSerializer.Deserialize<User>(jsonString, JsonOptions.DefaultOptions);
            var MyCourses = await _context.Enrollments
                                          .Include(e => e.Course)
                                          .Where(x => x.UserID == userId.UserID)
                                          .OrderByDescending(x => x.EnrollmentDate)
                                          .ToListAsync();
            if (!string.IsNullOrEmpty(searchString))
            {
                MyCourses = MyCourses.Where(c => c.Course.CourseName.Contains(searchString) || c.Course.CourseDescription.Contains(searchString)).ToList();
            }

            if (MyCourses == null || MyCourses.Count == 0)
            {
                ViewBag.Courses = null;
                return View();
            }

            ViewBag.Courses = MyCourses.Select(e => e.Course).ToList();
            return View(MyCourses.Select(e => e.Course));
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
                                       .Include(c=>c.courseSpecificQuestions)
                                       .Include(c => c.Enrollments)
                                       .FirstOrDefaultAsync(c => c.CourseID == id);

            if (course == null)
            {
                return NotFound();
            }

            ViewBag.Course = course;

            List<Comments> comments = await _context.comments.Include(x=>x.User).Where(c => c.CourseID == id && c.hideComment == false).ToListAsync();

            comments = comments.OrderByDescending(x=>x.commentdate).ToList();
            ViewBag.Comments = comments;
            return View(course);
        }


        public class Input
        {
            
            [Required]
            [StringLength(500)]
            public string commentText { get; set; }

            public string? commentphoto { get; set; }
            public IFormFile img_file { get; set; }
            public DateTime? commentdate { get; set; }
            public DateTime? commentUpdate { get; set; }
            // لو في تعليق مش حابين انه يظهر فنعمله هايد
            public bool? hideComment { get; set; } = false;
            public int? whoHideComment { get; set; }

            // لو عاوزين نظهر كومنت كان مخفي
            public bool? ShowHiddenComment { get; set; }
            public int? whoShowHiddenComment { get; set; }
            public int UserID { get; set; }
            [ForeignKey("UserID")]
            public User User { get; set; }

            public int CourseID { get; set; }
            [ForeignKey("CourseID")]
            public Course Course { get; set; }


            public int ?commentParentID { get; set; }
            [ForeignKey("commentParentID")]
            public Comments comments { get; set; }
        }
        [BindProperty]
        public Input input { get; set; }

        [HttpPost]
        public async Task<IActionResult> AddComment(int courseId, [FromForm] Input input)
        {
            if (HttpContext.Session.GetString("Login") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var userId = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("CurrentLoginUser")).UserID;

            if (input.img_file != null && input.img_file.Length > 0)
            {
                try
                {
                    // Get the wwwroot path
                    string uploadPath = Path.Combine(_host.WebRootPath, "CommentsImages");
                    Directory.CreateDirectory(uploadPath);
                    // Generate a unique filename for the uploaded file
                    string newfilename = $"{Guid.NewGuid()}{Path.GetExtension(input.img_file.FileName)}";
                    string fileName = Path.Combine(uploadPath, newfilename);

                    // Save the file to the server
                    using (var fileStream = new FileStream(fileName, FileMode.Create))
                    {
                        await input.img_file.CopyToAsync(fileStream);
                    }

                    input.commentphoto = Path.Combine("/CommentsImages/", newfilename);
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading file: " + ex.Message);
                }
            }

            Comments comment = new Comments
            {
                commentText = input.commentText,
                commentphoto = input.commentphoto,
                commentdate = DateTime.Now,
                UserID = userId,
                CourseID = courseId,
                commentParentID = input.commentParentID == 0 ? (int?)null : input.commentParentID // Check if the commentParentID is 0, if so, set to null
            };

            _context.comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("CourseDetails", new { id = courseId });
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion(int id, [FromForm] IFormFile img_file, [FromForm] string questionText, int parentID = 0)
        {
            if (HttpContext.Session.GetString("Login") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var userId = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("CurrentLoginUser")).UserID;
            string imagePath = null;

            if (img_file != null && img_file.Length > 0)
            {
                try
                {
                    string uploadPath = Path.Combine(_host.WebRootPath, "QuestionImages");
                    Directory.CreateDirectory(uploadPath);

                    string newFilename = $"{Guid.NewGuid()}{Path.GetExtension(img_file.FileName)}";
                    string filePath = Path.Combine(uploadPath, newFilename);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await img_file.CopyToAsync(fileStream);
                    }

                    imagePath = $"/QuestionImages/{newFilename}";
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Error uploading file: {ex.Message}");
                }
            }

            var courseSpecificQuestion = new CourseSpecificQuestions
            {
                CourseSpecificQuestionsDate = DateTime.Now,
                CourseSpecificQuestionsStates = false,
                CourseSpecificQuestionsText = questionText,
                CourseSpecificQuestionsParentID = parentID == 0 ? (int?)null : parentID, // Correctly handle null parent
                CourseSpecificQuestionsphoto = imagePath,
                UserID = userId,
                CourseID = id
            };

            _context.courseSpecificQuestions.Add(courseSpecificQuestion);
            await _context.SaveChangesAsync();

            return RedirectToAction("CourseDetails", new { id });
        }


    }
}
