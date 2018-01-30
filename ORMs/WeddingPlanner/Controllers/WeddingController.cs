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
    public class WeddingController : Controller
    {
        WeddingContext _context;
        public WeddingController(WeddingContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("wedding/show/{WeddingId}")]
        public IActionResult ShowWedding(int WeddingId)
        {
            // TempData["UserId"] = HttpContext.Session.GetInt32("UserId");
            // TempData["UserName"] = HttpContext.Session.GetString("UserName");
            ViewBag.Wedding = _context.Weddings.SingleOrDefault(wedding => wedding.WeddingId == WeddingId);
            ViewBag.Guests = _context.Guests.Where(guest => guest.WeddingId == WeddingId).Include(guest => guest.User).ToList();
            
            return View();
        }

        [HttpGet]
        [Route("wedding/add")]
        public IActionResult AddWedding()
        {
            TempData["UserId"] = HttpContext.Session.GetInt32("UserId");
            TempData["UserName"] = HttpContext.Session.GetString("UserName");
            return View();
        }

        [HttpPost]
        [Route("wedding/add")]
        public IActionResult CreateWedding(WeddingViewModel NewWedding, int UserId)
        {
            if (!ModelState.IsValid)
            {
                TempData["UserId"] = HttpContext.Session.GetInt32("UserId");
                TempData["UserName"] = HttpContext.Session.GetString("UserName");
                return View("AddWedding");
            }
            else
            {
                Wedding newWedding = new Wedding
                {
                    WedderOne = NewWedding.WedderOne,
                    WedderTwo = NewWedding.WedderTwo,
                    Date = NewWedding.Date,
                    Address = NewWedding.Address,
                    User = _context.Users.SingleOrDefault(user => user.UserId == UserId)
                };
                _context.Add(newWedding);
                _context.SaveChanges();
                return RedirectToAction("ShowDash", "User");
            }
        }

        [HttpGet]
        [Route("wedding/cancel/{WeddingId}")]
        public IActionResult CancelWedding(int WeddingId)
        {
            
            TempData["UserId"] = HttpContext.Session.GetInt32("UserId");
            Wedding Cancel = _context.Weddings.SingleOrDefault(wedding => wedding.WeddingId == WeddingId);

            _context.Remove(Cancel);
            _context.SaveChanges();
            return RedirectToAction("ShowDash", "User");
        }

        [HttpGet]
        [Route("wedding/rsvp/{WeddingId}")]
        public IActionResult RVSPWedding(int WeddingId)
        {
            TempData["UserId"] = HttpContext.Session.GetInt32("UserId");
            User NewGuest = _context.Users.SingleOrDefault(user => user.UserId == (int)TempData["UserId"]);
            Wedding JoinWedding = _context.Weddings.SingleOrDefault(wedding => wedding.WeddingId == WeddingId);

            Guest newGuest = new Guest()
            {
                UserId = NewGuest.UserId,
                WeddingId = JoinWedding.WeddingId,
                User = NewGuest,
                Wedding = JoinWedding
            };
            _context.Add(newGuest);
            _context.SaveChanges();
            return RedirectToAction("ShowDash", "User");
        }

        [HttpGet]
        [Route("wedding/unrsvp/{WeddingId}")]
        public IActionResult UnRVSPWedding(int WeddingId)
        {
            TempData["UserId"] = HttpContext.Session.GetInt32("UserId");
            Guest UnGuest = _context.Guests.SingleOrDefault(user => user.UserId == (int)TempData["UserId"]);

            _context.Remove(UnGuest);
            _context.SaveChanges();
            return RedirectToAction("ShowDash", "User");
        }
    }
}