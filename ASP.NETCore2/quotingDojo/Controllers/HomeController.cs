using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DbConnection;

namespace quotingDojo.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        [Route("/quotes")]
        public IActionResult Quotes()
        {
            List<Dictionary<string, object>> AllQuotes = DbConnector.Query("SELECT * FROM quotes");
            ViewBag.Quotes = AllQuotes;
            return View("quotes");
        }

        [HttpPost]
        [Route("/addQuote")]
        public IActionResult addQuote(string name, string quote)
        {
            DbConnector.Execute($"INSERT INTO quotes (name, quote, created_date) VALUES ('{name}', '{quote}', now())");

            return RedirectToAction("quotes");
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult delete(int id)
        {
            DbConnector.Execute($"DELETE FROM quotes where id = {id}");

            return RedirectToAction("quotes");
        }
    }
}