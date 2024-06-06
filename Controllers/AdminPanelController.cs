using courseManagementSystemV1.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace courseManagementSystemV1.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly AppDbContext _context;
        public AdminPanelController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }

            var acceptedUsersCount = _context.Users.Count(u => u.IsAccepted == true);
            var notAcceptedUsersCount = _context.Users.Count(u => u.IsAccepted == false);
            var hrCount = _context.Users.Count(u => u.IsUserHR == true);
            var instructorsCount = _context.Instructors.Count();
            var acceptedCoursesCount = _context.Courses.Count(c => c.IsAvailable == true);

            var statistics = new
            {
                AcceptedUsersCount = acceptedUsersCount,
                NotAcceptedUsersCount = notAcceptedUsersCount,
                HRCount = hrCount,
                InstructorsCount = instructorsCount,
                AcceptedCoursesCount = acceptedCoursesCount
            };

            return View(statistics);
        }

    }
}
