using Microsoft.AspNetCore.Mvc;

namespace courseManagementSystemV1.Controllers
{
    public class HRController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageCourses()
        {
            return View();
        }
    }
}
