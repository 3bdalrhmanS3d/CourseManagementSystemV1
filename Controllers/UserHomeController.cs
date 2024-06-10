using courseManagementSystemV1.DBContext;
using courseManagementSystemV1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace courseManagementSystemV1.Controllers
{
    public class UserHomeController : Controller
    {
        // for users
        // enroll in course, search, 

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
                                       .Include(c => c.courseRatings)
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
            var userId = JsonSerializer.Deserialize<User>(HttpContext.Session.Get("CurrentLoginUser"));
            var course = await _context.Courses.FindAsync(courseID);

            var Enrollments = new Enrollment
            {
                UserID = userId.UserID,
                CourseID = courseID,
                EnrollmentDate = DateTime.Now,
            };
            _context.Enrollments.Add(Enrollments);
            await _context.SaveChangesAsync();
            TempData["message"] = "Successfully enrolled in the course.";

            return RedirectToAction("Index");
        }

    }
}
