﻿using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using courseManagementSystemV1.DBContext;
using courseManagementSystemV1.Models;
using Microsoft.EntityFrameworkCore;

using System.Text.Json.Serialization;

namespace courseManagementSystemV1.Controllers
{
    public static class JsonOptions
    {
        public static JsonSerializerOptions DefaultOptions => new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            WriteIndented = true
        };
    }
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
        public async Task<IActionResult> Index(string email, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserEmail == email && x.UserPassword == password);

            if (user != null)
            {
                if (user.IsBlocked.HasValue && user.IsBlocked.Value == true )
                {
                    /*ViewBag.Message = "Your account is blocked.";*/
                    return View("Blocked");
                }
                if (!user.IsAccepted.HasValue || !user.IsAccepted.Value)
                {
                    return View("NotAccepted");
                }

                HttpContext.Session.SetString("Login", "true");
                HttpContext.Session.SetString("FullName", user.UserFirstName + " " + user.UserMiddelName + " " + user.UserLastName);
                HttpContext.Session.SetString("UserStatus", GetAccountType(user));

                // Use the custom JsonSerializerOptions
                HttpContext.Session.Set("CurrentLoginUser", JsonSerializer.SerializeToUtf8Bytes(user, JsonOptions.DefaultOptions));

                HttpContext.Session.SetString("Message", $"Welcome {user.UserFirstName} {user.UserLastName}");
                string userStatus = GetAccountType(user);
                VisitHistory newVisit = new VisitHistory
                {
                    UserID = user.UserID,
                    VisitHistoryDate = DateTime.Now,
                    CurrentVisitorStatus = userStatus
                };
                _context.VisitHistories.Add(newVisit);
                await _context.SaveChangesAsync();

                HttpContext.Session.SetString("UserStatus", userStatus);

                if (userStatus == "admin")
                {
                    return RedirectToAction("Index", "AdminPanel");
                }
                else if (userStatus == "HR")
                {
                    return RedirectToAction("Index", "HR");
                }
                else if (userStatus == "Instructor")
                {
                    return RedirectToAction("Index", "UserHome");
                }
                else if (userStatus == "Mentor")
                {
                    return RedirectToAction("Index", "UserHome");
                }
                else
                {
                    return RedirectToAction("Index", "UserHome");
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
            var instructor = _context.Instructors.FirstOrDefault(x => x.User.UserID == user.UserID);

            if (user.IsAdmin.HasValue && user.IsAdmin.Value)
            {
                return "admin";
            }
            else if (user.IsUserHR.HasValue && user.IsUserHR.Value)
            {
                return "HR";
            }
            else if (user.IsMentor.HasValue && user.IsMentor.Value)
            {
                return "Mentor";
            }
            else if (user.IsInstructor.HasValue && user.IsInstructor.Value == true)
            {
                if (instructor != null)
                {
                    if (instructor.IsAccepted.HasValue && instructor.IsAccepted == true)
                    {
                        return "Instructor";
                    }
                }
            }
            return "user";
        }
    }
}
