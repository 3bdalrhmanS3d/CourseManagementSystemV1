using courseManagementSystemV1.DBContext;
using courseManagementSystemV1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace courseManagementSystemV1.Controllers
{
       /*
            
        خاص بقبول المستخدمين ورفضهم ومسحهم والبلوك 
        */
    public class UsersManagementController : Controller
    {
        private readonly AppDbContext _context;

        public UsersManagementController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string section = "pending")
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }

            HttpContext.Session.SetString("Section", section);

            // جلب المستخدمين بناءً على حالتهم
            var usersNotAcceptedYet = await _context.Users.Where(u => u.IsAccepted == false && u.NormalUser == true && u.IsBlocked == false && u.IsDeleted == false && u.IsRejected == false).ToListAsync();
            var usersAccepted = await _context.Users.Where(u => u.IsAccepted == true && u.IsBlocked == false && u.IsDeleted == false && u.NormalUser == true).ToListAsync();
            var allUsersBlocked = await _context.Users.Where(p => p.IsBlocked == true && p.NormalUser == true).ToListAsync();
            var allUsersDeleted = await _context.Users.Where(p => p.IsDeleted == true && p.NormalUser == true).ToListAsync();
            var usersRejected = await _context.Users.Where(u => u.IsRejected == true).ToListAsync();

            // ترتيب وعرض البيانات في ViewBag
            ViewBag.AllUsersNotAcceptedYet = usersNotAcceptedYet.OrderByDescending(x => x.UserCreatedAccount).ToList();
            ViewBag.AllUsersAccepted = usersAccepted.OrderByDescending(x => x.userAcceptedDate).ToList();
            ViewBag.AllUsersBlocked = allUsersBlocked.OrderByDescending(x => x.userBlockedDate).ToList();
            ViewBag.AllUsersDeleted = allUsersDeleted.OrderByDescending(_ => _.userDeletedDate).ToList();
            ViewBag.usersRejected = usersRejected.OrderByDescending(_ => _.userRejectedDate).ToList();
            ViewBag.Section = section;

            return View();
        }

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
        public async Task<IActionResult> AcceptUser(int id)
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }

            var currentUser = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("CurrentLoginUser"));
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserID == id && u.IsAccepted == false && u.IsAdmin == false && u.IsUserHR == false);
            if (user != null)
            {
                user.IsAccepted = true;
                user.IsRejected = false;
                user.IsDeleted = false;
                user.IsBlocked = false;
                user.NormalUser = true;
                user.IsAdmin = false;
                user.IsUserHR = false;
                user.IsMentor = false;
                user.IsInstructor = false;
                user.userAcceptedDate = DateTime.Now;
                user.whoAcceptedUser = $"{currentUser.UserFirstName} {currentUser.UserMiddelName} {currentUser.UserLastName}";
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("Message", "User accepted!");
            }

            return RedirectToAction("Index", new { section = HttpContext.Session.GetString("Section") });
        }

        [HttpPost]
        public async Task<IActionResult> RejectUser(int id)
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }

            var currentUser = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("CurrentLoginUser"));
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserID == id && u.IsAccepted == false && u.IsAdmin == false && u.IsUserHR == false);
            if (user != null)
            {
                user.IsAccepted = false;
                user.IsRejected = true;
                user.IsDeleted = false;
                user.IsBlocked = false;
                user.NormalUser = false;
                user.IsAdmin = false;
                user.IsUserHR = false;
                user.IsMentor = false;
                user.IsInstructor = false;
                user.userRejectedDate = DateTime.Now;
                user.whoRejectedUser = $"{currentUser.UserFirstName} {currentUser.UserMiddelName} {currentUser.UserLastName}";
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("Message", "User rejected!");
            }

            return RedirectToAction("Index", new { section = HttpContext.Session.GetString("Section") });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }

            var currentUser = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("CurrentLoginUser"));
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserID == id);
            if (user != null)
            {
                user.IsDeleted = true;
                user.IsAdmin = false;
                user.IsUserHR = false;
                user.IsMentor = false;
                user.IsInstructor = false;
                user.userDeletedDate = DateTime.Now;
                user.whoDeletedUser = $"{currentUser.UserFirstName} {currentUser.UserMiddelName} {currentUser.UserLastName}";
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("Message", "User deleted!");
            }

            return RedirectToAction("Index", new { section = HttpContext.Session.GetString("Section") });
        }


        // block user
        [HttpPost]
        public async Task<IActionResult> BlockUser(int id)
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }

            var currentUser = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("CurrentLoginUser"));
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserID == id );
            if (user != null)
            {
                user.IsBlocked = true;
                user.IsAccepted = false;
                user.IsDeleted = false;

                user.userBlockedDate = DateTime.Now;
                user.whoBlockedUser = $"{currentUser.UserFirstName} {currentUser.UserMiddelName} {currentUser.UserLastName}";
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("Message", "User blocked!");
            }

            return RedirectToAction("Index", new { section = HttpContext.Session.GetString("Section") });
        }


        // رفع البلوك 
        [HttpPost]
        public async Task<IActionResult> UnBlockUser(int id)
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }

            var currentUser = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("CurrentLoginUser"));
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserID == id );
            if (user != null)
            {
                user.IsBlocked = false;
                user.IsAccepted = true;
                user.IsDeleted = false;
                user.NormalUser = true;

                
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("Message", "User unblocked!");
            }

            return RedirectToAction("Index", new { section = HttpContext.Session.GetString("Section") });
        }
    }
}
