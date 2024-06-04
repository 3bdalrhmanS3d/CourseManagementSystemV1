using courseManagementSystemV1.DBContext;
using courseManagementSystemV1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;
using System;

namespace courseManagementSystemV1.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        // make and delet admin
        #region
        public IActionResult MakeAdmin(int id)
        {
            var adminn = _context.Users.SingleOrDefault(x => x.UserID == id);
            var currentUser = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("CurrentLoginUser"));
            if (adminn != null)
            {
                adminn.IsAdmin = true;
                adminn.IsUserHR = false;
                adminn.NormalUser = false;

                adminn.whoAdminThisUser = currentUser.UserFirstName + " " + currentUser.UserLastName;
                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Admin done !");
            }
            return View();
        }
        #endregion

        // Delet , Bolck and Accept users
        #region
        public IActionResult UserAccepted(int id)
        {
            var unAccepted = _context.Users.SingleOrDefault(x=> x.UserID == id) ;
            var currentUser = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("CurrentLoginUser"));

            if (unAccepted != null)
            {
                unAccepted.IsAccepted = true;
                unAccepted.userAcceptedDate = DateTime.Now;
                unAccepted.whoAcceptedUser = currentUser.UserFirstName + " " + currentUser.UserLastName ;

                //_context.Users.Update(unAccepted);
                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Accepted done !");
            }
            return View();
        }


        public IActionResult UserDeleted(int id)
        {
            var currentUser = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("CurrentLoginUser"));
            var unDeleted = _context.Users.SingleOrDefault(x => x.UserID == id);
            if (unDeleted != null)
            {
                unDeleted.IsDeleted = true;
                unDeleted.userDeletedDate = DateTime.Now;
                unDeleted.whoDeletedUser = currentUser.UserFirstName + " " + currentUser.UserLastName;
                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Deleted done !");
            }
            return View();
        }

        public IActionResult UserBolcked(int id)
        {
            var currentUser = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("CurrentLoginUser"));
            var unBolcked = _context.Users.SingleOrDefault(x => x.UserID == id);
            if (unBolcked != null)
            {
                unBolcked.IsBlocked = true;
                unBolcked.userBlockedDate = DateTime.Now;
                unBolcked.whoBlockedUser = currentUser.UserFirstName + " " + currentUser.UserLastName;
                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Blocked done !");
            }
            return View();
        }

        #endregion

        // 
    }

}
