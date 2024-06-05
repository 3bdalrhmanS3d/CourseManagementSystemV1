using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using courseManagementSystemV1.DBContext;
using courseManagementSystemV1.Models;
using System.Linq;

namespace courseManagementSystemV1.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Login") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public IActionResult Index(string email, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserEmail == email && x.UserPassword == password);

            if (user != null)
            {
                if (user.IsBlocked.HasValue && user.IsBlocked.Value)
                {
                    ViewBag.Message = "Your account is blocked.";
                    return View();
                }

                if (!user.IsAccepted.HasValue || !user.IsAccepted.Value)
                {
                    return View("NotAccepted");
                }

                HttpContext.Session.SetString("Login", "true");
                HttpContext.Session.SetString("FullName", user.UserFirstName + " " + user.UserLastName);
                HttpContext.Session.SetString("UserStatus", GetAccountType(user));
                HttpContext.Session.Set("CurrentLoginUser", JsonSerializer.SerializeToUtf8Bytes(user));
                HttpContext.Session.SetString("Message", $"Welcome {user.UserFirstName} {user.UserLastName}");

                if (user.IsAdmin.HasValue && user.IsAdmin.Value)
                {
                    return RedirectToAction("Index", "AdminPanel");
                }
                else if (user.IsUserHR.HasValue && user.IsUserHR.Value)
                {
                    return RedirectToAction("Index", "HR");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.Message = "Invalid Email or Password";
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        private string GetAccountType(User user)
        {
            if (user.IsAdmin.HasValue && user.IsAdmin.Value)
            {
                return "admin";
            }
            else if (user.IsUserHR.HasValue && user.IsUserHR.Value)
            {
                return "HR";
            }
            else
            {
                return "user";
            }
        }
    }
}
