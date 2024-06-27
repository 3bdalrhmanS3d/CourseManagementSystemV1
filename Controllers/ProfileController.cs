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

            var jsonString = HttpContext.Session.GetString("CurrentLoginUser");
            if (jsonString != null)
            {
                try
                {
                    var person = JsonSerializer.Deserialize<User>(jsonString, JsonOptions.DefaultOptions);
                    return View(person);
                }
                catch (JsonException ex)
                {
                    // Log the exception details
                    Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                    Console.WriteLine($"JSON content: {jsonString}");
                    // Redirect to an error page or show an error message
                    return RedirectToAction("Error");
                }
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public IActionResult EditProfile()
        {
            if (HttpContext.Session.GetString("Login") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var jsonString = HttpContext.Session.GetString("CurrentLoginUser");
            if (jsonString != null)
            {
                try
                {
                    var person = JsonSerializer.Deserialize<User>(jsonString, JsonOptions.DefaultOptions);
                    return View(person);
                }
                catch (JsonException ex)
                {
                    // Log the exception details
                    Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                    Console.WriteLine($"JSON content: {jsonString}");
                    // Redirect to an error page or show an error message
                    return RedirectToAction("Error");
                }
            }
            return RedirectToAction("Index", "Login");
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

                HttpContext.Session.SetString("CurrentLoginUser", JsonSerializer.Serialize(user, JsonOptions.DefaultOptions));
                TempData["message"] = "Profile updated successfully.";
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
