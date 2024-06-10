using courseManagementSystemV1.DBContext;
using courseManagementSystemV1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace courseManagementSystemV1.Controllers
{
    public class UserCourseController : Controller
    {
        private readonly AppDbContext _context;

        public UserCourseController(AppDbContext context)
        {
            _context = context;
        }
        /*
         * كل الكورسات اللي هو مسجل فيها
            هيعرض الكورس لو الشخص مسجل فيه 
            وتفاصيله  
            الكومنتات 
            الكويزات 
            الملفات 
            اسئلة الاجابات 
            الدرجات الخاصة بالمستخدم في الكورس ده 
            وحضوره 
         */

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            if (HttpContext.Session.GetString("Login") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var currentUser = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("CurrentLoginUser"));
            var isEnrolled = await _context.Enrollments.AnyAsync(e => e.CourseID == id && e.UserID == currentUser.UserID);

            if (isEnrolled == false)
            {
                TempData["Message"] = "You must enroll in the course first.";
                return RedirectToAction("CourseDetails", "UserHome" ,id);
            }

            var course = await _context.Courses
                                       .Include(c => c.Enrollments)
                                       .Include(c => c.CourseManagements)
                                       .Include(c => c.Bonuses)
                                       .Include(c => c.hRManagements)
                                       .Include(c => c.courseRatings)
                                       .FirstOrDefaultAsync(c => c.CourseID == id);
            

            if (course == null)
            {
                return NotFound();
            }
            ViewBag.Course = course;

            return View(course);
        }

    }
}
