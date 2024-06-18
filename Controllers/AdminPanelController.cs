﻿using courseManagementSystemV1.DBContext;
using courseManagementSystemV1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
namespace courseManagementSystemV1.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly AppDbContext _context;
        public AdminPanelController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {

            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }


            // كل الناس 
            var admins = _context.Users.Where(p => p.IsAccepted == true && p.IsAdmin == false ).AsQueryable();
            ViewBag.allUsers = admins.OrderByDescending(x => x.userAcceptedDate).ToList();
            // كل الادمنز
            var ISadmins = _context.Users.Where(p => p.IsAdmin == true && p.IsAccepted == true).AsQueryable();
            ViewBag.admins = ISadmins.OrderByDescending(_ => _.userAcceptedDate).ToList();

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

        [HttpGet]
        public async Task<IActionResult> Visiting(string section = "pending", DateTime? startDate = null, DateTime? endDate = null, string userEmail = null, string userName = null, string userPhoneNumber = null, string userSSN = null)
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }

            HttpContext.Session.SetString("Section", section);

            // جلب كل بيانات الزيارات مع بيانات المستخدمين المرتبطة
            var allVisiting = _context.VisitHistories.Include(x => x.User).AsQueryable();

            // فلترة حسب تاريخ البداية والنهاية
            if (startDate.HasValue)
            {
                allVisiting = allVisiting.Where(v => v.VisitHistoryDate >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                allVisiting = allVisiting.Where(v => v.VisitHistoryDate <= endDate.Value);
            }

            if (!string.IsNullOrEmpty(userEmail))
            {
                allVisiting = allVisiting.Where(v => v.User.UserEmail == userEmail);
            }

            if (!string.IsNullOrEmpty(userName))
            {
                allVisiting = allVisiting.Where(v => (v.User.UserFirstName + " " + v.User.UserMiddelName + " " + v.User.UserLastName).Contains(userName));
            }

            if (!string.IsNullOrEmpty(userPhoneNumber))
            {
                allVisiting = allVisiting.Where(v => v.User.UserPhoneNumber == userPhoneNumber);
            }

            if (!string.IsNullOrEmpty(userSSN))
            {
                allVisiting = allVisiting.Where(v => v.User.UserSSN == userSSN);
            }

            var visitList = await allVisiting.ToListAsync();

            // تحديث UserLastVisit لكل مستخدم
            foreach (var visit in visitList)
            {
                var user = await _context.Users.FindAsync(visit.UserID);
                if (user != null)
                {
                    user.UserlastVisit = visit.VisitHistoryDate;
                }
            }
            await _context.SaveChangesAsync();
            ViewBag.Section = section;
            ViewBag.Visits = visitList.OrderByDescending(x => x.VisitHistoryDate).ToList();

            return View(visitList);
        }

        private string GetUserType(User user)
        {
            if (user.IsAdmin.HasValue && user.IsAdmin.Value)
                return "Admin";
            else if (user.IsUserHR.HasValue && user.IsUserHR.Value)
                return "HR";
            else
                return "Normal User";
        }

        [HttpPost]
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
                adminn.IsAccepted = true;
                adminn.IsBlocked=false;
                adminn.IsDeleted = false;
                adminn.IsAdmin = true;
                adminn.NormalUser = false;
                adminn.IsMentor = false;
                adminn.IsInstructor = false;
                adminn.IsUserHR = false;
                adminn.userAdmintedDate = DateTime.Now;
                adminn.whoAdminThisUser = $"{currentUser.UserFirstName}  {currentUser.UserMiddelName} {currentUser.UserLastName}";
                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Make Admin done !");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
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
                adminn.IsDeleted=true;
                adminn.IsAdmin=false;
                adminn.NormalUser = false;//
                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Delete admin done !");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult BlockAdmin(int id)
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }

            var adminn = _context.Users.SingleOrDefault(x => x.UserID == id);
            var currentUser = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("CurrentLoginUser"));
            if (adminn != null)
            {
                adminn.IsBlocked = true;
                adminn.IsAdmin = true;
                
                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Block admin done !");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveAdmin(int id)
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
                adminn.NormalUser = true;
                adminn.IsAccepted = true;
                adminn.IsDeleted=false;
                adminn.IsBlocked = false;
                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Delete admin done !");
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
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

        [HttpGet]
        public IActionResult MentorsAndInstructors()
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }

            // كل الناس 
            var allUsers = _context.Users
                .Where(p => p.IsAccepted == true && p.IsAdmin == false && p.IsUserHR == false && p.IsMentor == false && p.IsBlocked == false && p.IsDeleted == false && p.NormalUser == true)
                .OrderByDescending(x => x.userAcceptedDate)
                .ToList();
            ViewBag.AllUserss = allUsers;

            // المنتورز الحاليين
            var mentors = _context.Users.Where(u => u.IsMentor == true && u.IsAccepted == true && u.IsBlocked == false && u.IsDeleted == false ).ToList();
            // المنتورز الممسوحين
            var deletedMentors = _context.Users.Where(u => u.IsMentor == true && u.IsDeleted == true).ToList();
            // المنتورز المتبلكين
            var blockedMentors = _context.Users.Where(u => u.IsMentor == true && u.IsBlocked == true).ToList();

            // الإنستراكتورز الحاليين
            var instructors = _context.Users.Where(u => u.IsInstructor == true && u.IsAccepted == true && u.IsBlocked == false && u.IsDeleted == false).ToList();
            // الإنستراكتورز الممسوحين
            var deletedInstructors = _context.Instructors.Where(i => i.ISDeleted == true).ToList();
            // الإنستراكتورز المتبلكين
            var blockedInstructors = _context.Instructors.Where(i => i.IsBlocked == true).ToList();

            ViewBag.mentors = mentors;
            ViewBag.instructors = instructors;
            ViewBag.DeletedMentors = deletedMentors;
            ViewBag.BlockedMentors = blockedMentors;
            ViewBag.DeletedInstructors = deletedInstructors;
            ViewBag.BlockedInstructors = blockedInstructors;

            var notInstructors = _context.Users.Where(i => i.IsInstructor == false && i.IsAccepted == true && i.IsBlocked == false && i.IsDeleted == false).ToList();
            ViewBag.notInstructors = notInstructors;

            return View();
        }


        [HttpPost]
        public IActionResult MakeMentor(int id)
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }

            var user = _context.Users.SingleOrDefault(x => x.UserID == id);
            var currentUser = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("CurrentLoginUser"));

            if (user != null)
            {
                user.IsMentor = true;
                user.IsAccepted = true;
                user.IsBlocked = false;
                user.IsDeleted = false;
                user.NormalUser = false;
                user.userbeMentorDate = DateTime.Now;

                user.whoMentorUser = $"{currentUser.UserFirstName} {currentUser.UserMiddelName} {currentUser.UserLastName}";                
            }
            _context.SaveChanges();
            HttpContext.Session.SetString("Message", "Mentor assigned successfully!");

            return RedirectToAction("MentorsAndInstructors");
        }

        [HttpPost]
        public IActionResult NormalUser(int id) // remove
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }

            var user = _context.Users.SingleOrDefault(x => x.UserID == id);
            if (user != null)
            {
                user.IsMentor = false;
                user.IsInstructor = false;
                user.IsAccepted = true;
                user.IsBlocked = false;
                user.IsDeleted = false;
                user.NormalUser = true;

                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "User status reverted successfully!");
            }

            return RedirectToAction("MentorsAndInstructors");
        }


        /*[HttpPost]
        public async Task<IActionResult> AcceptInstructor(int id)
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }
            var user = await _context.Instructors.SingleOrDefaultAsync(x => x.UserID == id);

            if (user != null)
            {
                user.IsInstructor = true;
                user.IsAccepted = true;
                user.instructorDateAt = DateTime.Now;
                
            }
            _context.SaveChanges();
            HttpContext.Session.SetString("Message", "Instructor assigned successfully!");

            return RedirectToAction("MentorsAndInstructors");

        }*/

        [HttpPost]
        public IActionResult MakeInstructor(int id)
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }

            var user = _context.Users.SingleOrDefault(x => x.UserID == id);
            
            if (user != null)
            {
                user.NormalUser = false;
                user.IsInstructor = true;
                user.IsAccepted = true;
                user.IsBlocked = false;
                user.IsDeleted = false;
                user.IsMentor = false;

                /*_context.SaveChanges();*/
                var instructor = new Instructor
                {
                    UserID = user.UserID,
                    IsInstructor = true,
                    instructorDateAt = DateTime.Now,
                    IsAccepted = true
                };

                _context.Instructors.Add(instructor);
                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Instructor assigned successfully!");
            }

            return RedirectToAction("MentorsAndInstructors");
        }

        [HttpPost]
        public IActionResult BlockMentor(int id)
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }
            var currentUser = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("CurrentLoginUser"));

            var user = _context.Users.SingleOrDefault(x => x.UserID == id);
            var courseManagement = _context.courseManagements.SingleOrDefault(x => x.instructorID == id);

            if (courseManagement != null)
            {
                courseManagement.Isaccepted = false;
                _context.SaveChanges();
            }

            if (user != null)
            {
                user.IsBlocked = true;
                user.IsMentor = true;

                user.userBlockedDate    = DateTime.Now;
                user.whoBlockedUser = $"{currentUser.UserFirstName} {currentUser.UserMiddelName} {currentUser.UserLastName}";

                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Mentor blocked successfully!");
            }

            return RedirectToAction("MentorsAndInstructors");
        }

        [HttpPost]
        public IActionResult BlockInstructor(int id)
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }
            var user = _context.Users.SingleOrDefault(x => x.UserID == id);

            user.IsBlocked = true ;
            user.IsInstructor = true ;
            _context.SaveChanges();

            var instructor = _context.Instructors.SingleOrDefault(x => x.UserID == id);
            if (instructor != null)
            {
                instructor.IsBlocked = true;
                instructor.IsInstructor = true;

                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Instructor blocked successfully!");
            }

            return RedirectToAction("MentorsAndInstructors");
        }

        [HttpPost]
        public IActionResult DeleteMentor(int id)
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }

            var user = _context.Users.SingleOrDefault(x => x.UserID == id);
            if (user != null)
            {
                user.IsDeleted = true;
                user.IsMentor = false;
                user.NormalUser = true;

                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Mentor deleted successfully!");
            }

            return RedirectToAction("MentorsAndInstructors");
        }

        [HttpPost]
        public IActionResult DeleteInstructor(int id)
        {
            if (HttpContext.Session.GetString("Login") != "true" || HttpContext.Session.GetString("UserStatus") != "admin")
            {
                return RedirectToAction("Index", "Login");
            }
            var currentUser = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("CurrentLoginUser"));

            var user = _context.Users.SingleOrDefault(x => x.UserID == id);
            user.IsDeleted = true;
            user.IsInstructor = false;
            user.NormalUser = true;
            user.userDeletedDate = DateTime.Now;
            user.whoDeletedUser = $"{currentUser.UserFirstName} {currentUser.UserMiddelName} {currentUser.UserLastName}";
            

            _context.SaveChanges();
            var instructor = _context.Instructors.SingleOrDefault(x => x.UserID == id);
            if (instructor != null)
            {
                instructor.ISDeleted = true;
                instructor.IsInstructor = false;
                
                _context.SaveChanges();
                HttpContext.Session.SetString("Message", "Instructor deleted successfully!");
            }

            return RedirectToAction("MentorsAndInstructors");
        }
    }
}
