using Microsoft.AspNetCore.Mvc;

namespace courseManagementSystemV1.Controllers
{
    public class MentorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
