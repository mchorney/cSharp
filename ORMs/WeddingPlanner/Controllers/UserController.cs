using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace WeddingPlanner.Controllers
{
    public class UserController : Controller
    {
        WeddingContext _context;
        public UserController(WeddingContext context)
        {
            _context = context;
        }

        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("dashboard")]
        public IActionResult ShowDash()
        {
            TempData["UserId"] = HttpContext.Session.GetInt32("UserId");
            TempData["UserName"] = HttpContext.Session.GetString("UserName");
            List<Wedding> AllWeddings = _context.Weddings.Include(w => w.Guests).ThenInclude(w => w.User).ToList();
            List<Guest> guests = _context.Guests.Include(g => g.Wedding).ThenInclude(w => w.User).ToList();
            System.Console.WriteLine(guests.Count);
            System.Console.WriteLine(TempData["UserName"]);
            System.Console.WriteLine(TempData["UserId"]);
            bool GuestLoggedIn = false;
            foreach(var wedding in AllWeddings)
            {
                foreach(var guest in wedding.Guests)
                {
                    System.Console.WriteLine(guest.User.FirstName);
                    if(guest.UserId == (int)TempData["UserId"])
                    {
                        GuestLoggedIn = true;
                    }
                }
            }
            ViewBag.Weddings = AllWeddings;
            ViewBag.Guest = GuestLoggedIn;
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(UserViewModel RegUser)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            else
            {
                User UserCheck = _context.Users.SingleOrDefault(user => user.Email == RegUser.RegisterUser.Email);
                if (UserCheck != null)
                {
                    ViewBag.EmailExists = "That user already exists!";
                    return View("Index");
                }
                else
                {
                    PasswordHasher<Register> hasher = new PasswordHasher<Register>();
                    string HashedPW = hasher.HashPassword(RegUser.RegisterUser, RegUser.RegisterUser.Password);
                    User NewUser = new User
                    {
                        FirstName = RegUser.RegisterUser.FirstName,
                        LastName = RegUser.RegisterUser.LastName,
                        Email = RegUser.RegisterUser.Email,
                        Password = HashedPW,
                    };
                    _context.Add(NewUser);
                    _context.SaveChanges();

                    User LoggedIn = _context.Users.SingleOrDefault(user => user.Email == RegUser.RegisterUser.Email);
                    HttpContext.Session.SetInt32("UserId", LoggedIn.UserId);
                    HttpContext.Session.SetString("UserName", LoggedIn.FirstName);
                    return RedirectToAction("ShowDash");
                }
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(UserViewModel LogUser)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            else
            {
                User LoggingIn = _context.Users.SingleOrDefault(user => user.Email == LogUser.LoginUser.Email);
                if (LoggingIn == null)
                {
                    ViewBag.NoUser = "It looks like you need to register!";
                    return View("Index");
                }
                else
                {
                    PasswordHasher<Login> hasher = new PasswordHasher<Login>();
                    if (hasher.VerifyHashedPassword(LogUser.LoginUser, LoggingIn.Password, LogUser.LoginUser.Password) == 0)
                    {
                        ViewBag.NoUser = "Invalid Email or Password";
                        return View("Index");
                    }
                    else
                    {
                        //Grabbing User from DB and putting into session
                        User LoggedIn = _context.Users.SingleOrDefault(user => user.Email == LogUser.LoginUser.Email);
                        HttpContext.Session.SetInt32("UserId", LoggedIn.UserId);
                        HttpContext.Session.SetString("UserName", LoggedIn.FirstName);
                        ViewBag.NoUser = "";
                        return RedirectToAction("ShowDash");
                    }
                }
            }
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
