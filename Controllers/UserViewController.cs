using courseManagementSystemV1.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace courseManagementSystemV1.Controllers
{
    public class UserViewController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _host;

        public UserViewController(AppDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
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
    }
}
