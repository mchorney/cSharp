using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DbConnection;

namespace ajaxNotes.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            string query = $"SELECT * FROM NOTES ORDER BY ID DESC";
            List<Dictionary<string, object>> allNotes = DbConnector.Query(query);
            ViewBag.notes = allNotes;
            return View();
        }

        [HttpPost]
        [Route("/addNote")]
        public string AddNote(string title)
        {
            string query = $"INSERT INTO NOTES (TITLE) VALUES ('{title}')";
            var result = DbConnector.Insert(query);
            return result;
        }

        [HttpPost]
        [Route("/updateNote")]
        public string UpdateNote(string id, string note)
        {
            string query = $"UPDATE NOTES SET NOTE = '{note}' WHERE ID = {id}";
            DbConnector.Execute(query);
            return "ok";
        }

        [HttpPost]
        [Route("/delete")]
        public string delete(string ID)
        {
            string query = $"DELETE FROM NOTES WHERE ID = '{ID}'";
            DbConnector.Execute(query);
            //return RedirectToAction("Index");
            return "ok";
        }
    }
}
