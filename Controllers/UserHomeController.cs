using courseManagementSystemV1.DBContext;
using courseManagementSystemV1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace courseManagementSystemV1.Controllers
{
    public class UserHomeController : Controller
    {
        private readonly AppDbContext _context;

        public UserHomeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            if (HttpContext.Session.GetString("Login") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var courses = from c in _context.Courses
                          where c.IsAvailable == true
                          select c;

            if (!string.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(c => c.CourseName.Contains(searchString) || c.CourseDescription.Contains(searchString));
            }

            var sortedCourses = await courses.OrderByDescending(c => c.CourseStartDate).ToListAsync();

            if (sortedCourses == null)
            {
                return NotFound();
            }

            ViewBag.Courses = sortedCourses;
            ViewBag.SearchString = searchString;

            return View(sortedCourses);
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
                                        .ThenInclude(comment => comment.User)
                                       .Include(c => c.Enrollments)
                                       .FirstOrDefaultAsync(c => c.CourseID == id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> EnrollInCourse(int courseID)
        {
            if (HttpContext.Session.GetString("Login") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var jsonString = HttpContext.Session.GetString("CurrentLoginUser");
            if (jsonString != null)
            {
                var userId = JsonSerializer.Deserialize<User>(jsonString, JsonOptions.DefaultOptions);
                var course = await _context.Courses.FindAsync(courseID);

                if (course == null)
                {
                    return NotFound();
                }

                var existingEnrollment = await _context.Enrollments
                                                       .FirstOrDefaultAsync(e => e.UserID == userId.UserID && e.CourseID == courseID);

                if (existingEnrollment != null)
                {
                    ViewBag.Message = "You are already enrolled in this course.";
                    return RedirectToAction("Index");
                }

                var newEnrollment = new Enrollment
                {
                    UserID = userId.UserID,
                    CourseID = courseID,
                    EnrollmentDate = DateTime.Now,
                };
                _context.Enrollments.Add(newEnrollment);
                await _context.SaveChangesAsync();

                ViewBag.Message = "Successfully enrolled in the course.";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Login");
        }
    }
}