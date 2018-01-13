using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace callingCard.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("{fname}/{lName}/{Age}/{color}")]
        public JsonResult Index(string fname, string lName, int Age, string color)
        {
            var AnonObject = new 
            {
                firstName = fname,
                lastName = lName,
                age = Age,
                Color = color,
            };
            return Json(AnonObject);
        }
    }
}