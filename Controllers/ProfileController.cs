using courseManagementSystemV1.DBContext;
using courseManagementSystemV1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace courseManagementSystemV1.Controllers
{
    public class ProfileController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _host;

        public ProfileController(AppDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Login") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var person = JsonSerializer.Deserialize<User>(HttpContext.Session.Get("CurrentLoginUser"));
            return View(person);
        }

        [HttpGet]
        public IActionResult EditProfile()
        {
            if (HttpContext.Session.GetString("Login") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var person = JsonSerializer.Deserialize<User>(HttpContext.Session.Get("CurrentLoginUser"));
            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(User model)
        {
            if (HttpContext.Session.GetString("Login") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var user = await _context.Users.FindAsync(model.UserID);
            if (user != null)
            {
               
                user.UserEmail = model.UserEmail;
                user.UserPassword = model.UserPassword;
                user.UserConfPassword = model.UserConfPassword;

                await _context.SaveChangesAsync();
                HttpContext.Session.Set("CurrentLoginUser", JsonSerializer.SerializeToUtf8Bytes(user));
                TempData["message"] = "Profile updated successfully.";
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
