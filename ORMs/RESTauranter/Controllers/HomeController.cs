using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RESTauranter.Models;
using System.Linq;

namespace RESTauranter.Controllers
{
    public class HomeController : Controller
{
    private RestauranterContext _context;
 
    public HomeController(RestauranterContext context)
    {
        _context = context;
    }
 
    [HttpGet]
    [Route("")]
    public IActionResult Index()
    {   
        return View();
    }


    [HttpPost]
    [Route("processForm")]
    public IActionResult processForm(Review review)
    {
        if(TryValidateModel(review))
        {
            _context.Add(review);
            _context.SaveChanges();
            return Redirect("reviews");
        } else {
            // REPORT ERRORS
            Console.WriteLine("ERRORS during validation");
            ViewBag.errors = ModelState.Values;
            return View("index", review);
        }
    }

    [HttpGet]
    [Route("reviews")]
    public IActionResult reviews()
    {
        List<Review>ReturnedValues = _context.reviews.OrderByDescending(review => review.Id).ToList();
        ViewBag.Reviews=ReturnedValues;
        return View("reviews");
    }
}
}
