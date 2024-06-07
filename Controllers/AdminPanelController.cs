using courseManagementSystemV1.DBContext;
using courseManagementSystemV1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
namespace courseManagementSystemV1.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly AppDbContext _context;
        public AdminPanelController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {

            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }


            // كل الناس 
            var admins = _context.Users.Where(p => p.IsAccepted == true && p.IsAdmin == false).AsQueryable();
            ViewBag.allUsers = admins.OrderByDescending(x => x.userAcceptedDate).ToList();
            // كل الادمنز
            var ISadmins = _context.Users.Where(p => p.IsAdmin == true && p.IsAccepted == true).AsQueryable();
            ViewBag.admins = ISadmins.OrderByDescending(_ => _.userAcceptedDate).ToList();

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

        /*[HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }
            
            return View();
        }*/

        [HttpGet]
        public async Task<IActionResult> ViewData(int id)
        {
            var user = await _context.Users
                            .Include(u => u.Enrollments)
                                .ThenInclude(e => e.Course)
                            .Include(u => u.Enrollments)
                                .ThenInclude(e => e.Attendances)
                            .Include(u => u.Bonuses)
                            .FirstOrDefaultAsync(u => u.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult MakeAdmin(int id)
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }

            var adminn = _context.Users.SingleOrDefault(x => x.UserID == id);
            var currentUser = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("CurrentLoginUser"));
            if (adminn != null)
            {
                adminn.IsAdmin = true;
                adminn.IsUserHR = false;
                adminn.NormalUser = false;

                adminn.whoAdminThisUser = $"{currentUser.UserFirstName}  {currentUser.UserMiddelName} {currentUser.UserLastName}";
                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Make Admin done !");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteAdmin(int id)
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }

            var adminn = _context.Users.SingleOrDefault(x => x.UserID == id);
            var currentUser = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("CurrentLoginUser"));
            if (adminn != null)
            {
                adminn.IsAdmin = false;
                adminn.IsUserHR = false;
                adminn.IsAccepted = false;
                adminn.NormalUser = true;
                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Delete admin done !");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Statistics()
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
