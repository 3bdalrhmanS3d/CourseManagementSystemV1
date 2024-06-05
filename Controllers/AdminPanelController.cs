using courseManagementSystemV1.DBContext;
using courseManagementSystemV1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;
using System;

namespace courseManagementSystemV1.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly AppDbContext _context;

        public AdminPanelController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
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
        // make and delet admin
        #region

        public IActionResult AdminManagement()
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }
            // كل الناس 
            var admins = _context.Users.Where(p => p.IsAccepted == true).AsQueryable();
            ViewBag.allUsers = admins.OrderByDescending(x=>x.userAcceptedDate).ToList();
            // كل الادمنز
            var ISadmins = _context.Users.Where(p=>p.IsAdmin == true).AsQueryable();
            ViewBag.admins = ISadmins.OrderByDescending(_ => _.userAcceptedDate).ToList();

            return View();
        
        }
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

                adminn.whoAdminThisUser = currentUser.UserFirstName + " " + currentUser.UserMiddelName + " " + currentUser.UserLastName;
                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Make Admin done !");
            }
            return View();
        }
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
                adminn.NormalUser = true;

                adminn.whoAdminThisUser = currentUser.UserFirstName + " " + currentUser.UserMiddelName + " " + currentUser.UserLastName;
                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Delete admin done !");
            }

            return View();
        }
        #endregion

        // Delet , Bolck and Accept users
        #region

        public IActionResult UserManagement()
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }
            // لم يقبل بعد
            var AllUsersNotAcceptedYet = _context.Users.Where(p => p.IsAccepted == false).AsQueryable();
            if(AllUsersNotAcceptedYet == null)
            {
                ViewBag.AllUsersNotAcceptedYet = "NO data";
            }
            else
            {
                ViewBag.AllUsersNotAcceptedYet = AllUsersNotAcceptedYet.OrderByDescending(x => x.UserCreatedAccount).ToList();
            }

            // accepted
            var AllUsersNotAccepted = _context.Users.Where(p => p.IsAccepted == true).AsQueryable();
            ViewBag.AllUsersNotAccepted = AllUsersNotAccepted.OrderByDescending(_ => _.userAcceptedDate).ToList();

            // blocked
            var AllUsersBlocked = _context.Users.Where(p => p.IsBlocked == true).AsQueryable();
            ViewBag.AllUsersBlocked = AllUsersBlocked.OrderByDescending(_ => _.userBlockedDate).ToList();

            return View();

        }
        public IActionResult UserAccepted(int id)
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }

            var unAccepted = _context.Users.SingleOrDefault(x=> x.UserID == id) ;
            var currentUser = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("CurrentLoginUser"));

            if (unAccepted != null)
            {
                unAccepted.IsAccepted = true;
                unAccepted.userAcceptedDate = DateTime.Now;
                unAccepted.whoAcceptedUser = currentUser.UserFirstName + " " + currentUser.UserMiddelName + " " + currentUser.UserLastName;

                //_context.Users.Update(unAccepted);
                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Accepted done !");
            }
            return RedirectToAction("UserManagement");
        }


        public IActionResult UserDeleted(int id)
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }
            var currentUser = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("CurrentLoginUser"));
            var unDeleted = _context.Users.SingleOrDefault(x => x.UserID == id);
            if (unDeleted != null)
            {
                unDeleted.IsDeleted = true;
                unDeleted.userDeletedDate = DateTime.Now;
                unDeleted.whoDeletedUser = currentUser.UserFirstName + " " + currentUser.UserMiddelName + " " + currentUser.UserLastName;
                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Deleted done !");
            }
            return RedirectToAction("UserManagement");
        }

        public IActionResult UserBolcked(int id)
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }
            var currentUser = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("CurrentLoginUser"));
            var unBolcked = _context.Users.SingleOrDefault(x => x.UserID == id);
            if (unBolcked != null)
            {
                unBolcked.IsBlocked = true;
                unBolcked.userBlockedDate = DateTime.Now;
                unBolcked.whoBlockedUser = currentUser.UserFirstName + " " + currentUser.UserMiddelName + " " + currentUser.UserLastName;
                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Blocked done !");
            }
            return RedirectToAction("UserManagement");
        }

        #endregion

        // courses
        #region
        public IActionResult AcceptanceOfCourses(int id)
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }
            var currentUser = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("CurrentLoginUser"));
            var course = _context.Courses.SingleOrDefault(x=>x.CourseID == id);
            if (course != null)
            {
                course.IsAvailable = true;
                course.whoAcceptedCourse = currentUser.UserFirstName + " " + currentUser.UserMiddelName + " " + currentUser.UserLastName;
                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Course accepted !");
            }

            return RedirectToAction("CourseManagement");
        }

        public IActionResult FinishTheCourse(int id)
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }
            var course = _context.Courses.SingleOrDefault(x => x.CourseID == id && x.IsAvailable == true);
            if (course != null) 
            {
                course.IsAvailable = false;
                course.CourseEndDate = DateTime.Now;

                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Course End!");
            }
            return RedirectToAction("CourseManagement");
        }
        #endregion

        // statistics
        #region
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
        #endregion
    }

}
