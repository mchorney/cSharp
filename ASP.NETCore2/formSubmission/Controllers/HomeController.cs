using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FormSubmission.Models;

namespace FormSubmission.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.noErrors = false;
            return View();
        }

        [HttpPost]
        [Route("processForm")]
        public IActionResult processForm(string firstName, string lastName, int Age, string email, string password){
            User newUser = new User{
                firstName = firstName,
                lastName = lastName,
                Age = Age,
                email = email,
                password = password
            };
            TryValidateModel(newUser);
            ViewBag.errors = ModelState.Values;
            return View("success");
        }

        [HttpGet]
        [Route("/success")]
        public IActionResult success()
        {
            return View("success");
        }
    }
}
