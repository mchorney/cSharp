
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using loginReg.Models;
using DbConnection;

namespace loginReg.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            // Check for/print errors
            if((string)TempData["errors"] != ""){
                return View();
            }
            else{
                TempData["errors"] = "";
            }
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult register(User newUser)
        {
            if(ModelState.IsValid)
            {
                List<Dictionary<string, object>> isUser = DbConnector.Query($"SELECT * FROM users WHERE email = '{newUser.email}'");
                if(isUser.Count == 0){
                    DbConnector.Execute($"INSERT INTO users(firstName, lastName, email, password) VALUES('{newUser.firstName}', '{newUser.lastName}', '{newUser.email}', '{newUser.password}')");
                    List<Dictionary<string, object>> loggedIn = DbConnector.Query("SELECT LAST_INSERT_ID()");
                    HttpContext.Session.SetInt32("loggedIn", Convert.ToInt32(loggedIn[0]["LAST_INSERT_ID()"]));
                    return Redirect("home");
                }else{
                    TempData["errors"] = "Email already registered.";
                    return Redirect("/");
                }
            }else{
                return View("index", newUser);
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult login(string email, string password){
            List<Dictionary<string, object>> isUser = DbConnector.Query($"SELECT * FROM users WHERE email = '{email}'");
            if(isUser.Count == 0){
                TempData["errors"] = "Invalid email address.";
                return Redirect("/");
            }else if((string)isUser[0]["password"] != password) {
                TempData["errors"] = "Incorrect Password.";
                return Redirect("/");
            }else{
                HttpContext.Session.SetInt32("loggedIn", (int)isUser[0]["id"]);
                return Redirect("home");
            }
        }

        [HttpGet]
        [Route("home")]
        public IActionResult success()
        {
            return View("home");
        }
    }
}
