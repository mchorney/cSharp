
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using loginReg.Models;
using DbConnection;

namespace theWall.Controllers
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
                return RedirectToAction("success");
            }
        }

        [HttpGet]
        [Route("home")]
        public IActionResult success()
        {
            
            if (HttpContext.Session.GetInt32("loggedIn")==null) 
            {
                return View("index");
            }
            else {
            List<Dictionary<string, object>> allMessages = DbConnector.Query("SELECT concat(firstName, ' ', lastName) fullName, date_format(messages.created_at, '%M %D %Y') as date, message, messages.created_at, messages.id FROM messages join users on user_id=users.id order by created_at DESC");

            ViewBag.Messages = allMessages;

            List<Dictionary<string, object>> allComments = DbConnector.Query("SELECT concat(firstName, '', lastName) fullName, date_format(comments.created_at, '%M %D %Y') as date, time_format(comments.created_at, '%r') as time, comments.created_at, comment, message_id from users join comments on users.id=user_id order by created_at ASC");

            ViewBag.Comments = allComments;

            return View("home");
        }
        }

        [HttpPost]
        [Route("postMsg")]
        public IActionResult postMsg(string post)
        {
            int? msgID = HttpContext.Session.GetInt32("loggedIn");

            Console.WriteLine("Logged in ID is: " + msgID);
        
            string query = $"INSERT INTO messages(user_id, message, created_at) VALUES ({msgID}, '{post}', NOW() )";

            post = post.Replace("'", "\'");
        
            DbConnector.Execute(query);

            return Redirect("home");
        }

        [HttpPost]
        [Route("postComment")]
        public IActionResult postComment(string comment, int msgID)
        {
            int? commentID = HttpContext.Session.GetInt32("loggedIn");
            Console.WriteLine("***********" + comment + "***********");
            // comment=comment.Replace("'", "\'");
            DbConnector.Execute($"INSERT INTO comments(message_id, user_id, comment, created_at) VALUES ('{msgID}', {commentID}, '{comment}', NOW())");

            return Redirect("home");
        }

        [HttpGet]
        [Route("logout")]

        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return Redirect("home");
        }
    }
}