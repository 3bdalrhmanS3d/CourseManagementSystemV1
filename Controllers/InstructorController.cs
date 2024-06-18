using Microsoft.AspNetCore.Mvc;

namespace courseManagementSystemV1.Controllers
{
    public class InstructorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
