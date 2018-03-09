using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using BankAccounts.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace BankAccounts.Controllers
{
    public class UserController : Controller
    {
        BankContext _context;
        public UserController(BankContext context)
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
        [Route("home")]
        public IActionResult UserHome()
        {
            TempData["id"] = HttpContext.Session.GetInt32("UserId");
            TempData["UserName"] = HttpContext.Session.GetString("UserName");
            if(TempData["id"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                List<Account> UserAccounts = _context.Accounts.Where(account => account.UserId == (int)TempData["id"]).Include(account => account.User).ToList();
                ViewBag.Accounts = UserAccounts;
                return View();
            }
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
                    if(UserCheck != null)
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
                        return RedirectToAction("UserHome");
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
                            return RedirectToAction("UserHome");
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
