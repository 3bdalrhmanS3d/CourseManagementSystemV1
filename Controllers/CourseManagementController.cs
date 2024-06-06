using courseManagementSystemV1.DBContext;
using courseManagementSystemV1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace courseManagementSystemV1.Controllers
{
    public class CourseManagementController : Controller
    {
        public readonly AppDbContext _context;
        public CourseManagementController(AppDbContext context) { _context = context; }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }

            var coursesNotAcceptedYet = await _context.Courses.Where(x => x.IsAvailable == false).ToListAsync();
            var coursesNotAccepted = await _context.Courses.Where(x => x.IsAvailable == true).ToListAsync();
            ViewBag.CoursesNotAccepted = coursesNotAccepted.OrderByDescending(x => x.CourseStartDate).ToList();
            ViewBag.CoursesNotAcceptedYet = coursesNotAcceptedYet.OrderByDescending(x => x.CourseStartDate).ToList();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewDataCourse(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Enrollments)
                .ThenInclude(e => e.User)
                .Include(c => c.CourseManagements)
                .ThenInclude(cm => cm.instructor)
                .FirstOrDefaultAsync(c => c.CourseID == id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpPost]
        public IActionResult AcceptanceOfCourses(int id)
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }

            var currentUser = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("CurrentLoginUser"));
            var course = _context.Courses.SingleOrDefault(x => x.CourseID == id && x.IsAvailable == false);
            if (course != null)
            {
                course.IsAvailable = true;
                course.whoAcceptedCourse = $"{ currentUser.UserFirstName}  {currentUser.UserMiddelName} {currentUser.UserLastName}";
                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Course accepted!");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult FinishTheCourse(int id)
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }

            var course = _context.Courses.SingleOrDefault(x => x.CourseID == id && x.IsAvailable == true);
            if (course != null)
            {
                course.IsAvailable = false;
                course.CourseEndDate = DateTime.Now;

                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Course End!");
            }

            return RedirectToAction("Index");
        }
    }
}
