using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using BrightIdeas.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace BrightIdeas.Controllers
{
    public class PostController : Controller
    {
        private Context _context;
        public PostController(Context context) {
            _context = context;
        }

        [HttpGet]
        [Route("Dashboard")]
        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Index", "User");
            }
            User CurrentUser = _context.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            List<Post> AllPosts = _context.Posts
                                    .Include(p => p.Likes)
                                        .ThenInclude(l => l.Liker)
                                    .Include(p => p.Poster)
                                    .OrderByDescending(p => p.Likes.Count)
                                    .ToList();
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.AllPosts = AllPosts;
            ViewBag.CurrentUser = CurrentUser;
            return View();
        }

        [HttpPost]
        [Route("AddPost")]
        public IActionResult AddPost(Post model) {
            if(HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Index", "User");
            }
            User CurrentUser = _context.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            if(ModelState.IsValid) {
                model.UserId = CurrentUser.UserId;
                model.Poster = CurrentUser;
                _context.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            return View("Dashboard");
        }

        [HttpGet]
        [Route("Like/{PostId}")]
        public IActionResult Like(int PostId) {
            if(HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Index", "User");
            }
            User CurrentUser = _context.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            Post CurrentPost = _context.Posts
                                        .Include(p => p.Likes)
                                            .ThenInclude(l => l.Liker)
                                        .Include(p => p.Poster)
                                        .SingleOrDefault(p => p.PostId == PostId);
            foreach(var like in CurrentPost.Likes) {
                if(like.Liker == CurrentUser) {
                    ModelState.AddModelError("Liker", "Already Liked Post");
                }
            }
            if(ModelState.IsValid) {
                Like NewLike = new Like {
                    UserId = CurrentUser.UserId,
                    Liker = CurrentUser,
                    PostId = PostId,
                    Post = CurrentPost
                };
                CurrentPost.Likes.Add(NewLike);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("Post/{PostId}")]
        public IActionResult Post(int PostId) {
            if(HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Index", "User");
            }
            Post CurrentPost = _context.Posts
                                        .Include(p => p.Likes)
                                            .ThenInclude(l => l.Liker)
                                        .Include(p => p.Poster)
                                        .SingleOrDefault(p => p.PostId == PostId);
            ViewBag.CurrentPost = CurrentPost;
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            return View("Post");
        }

        [HttpGet]
        [Route("/Users/{UserId}")]
        public IActionResult UserProfile(int UserId) {
            if(HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Index", "User");
            }
            User UserProfile = _context.Users
                                        .Include(u => u.Likes)
                                        .Include(u => u.Posts)
                                        .SingleOrDefault(u => u.UserId == UserId);
            ViewBag.UserProfile = UserProfile;
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            return View("UserProfile");
        }

        [HttpGet]
        [Route("Delete/{PostId}")]
        public IActionResult Delete(int PostId) {
            if(HttpContext.Session.GetInt32("UserId") == null) {
                return RedirectToAction("Index", "User");
            }
            Post CurrentPost = _context.Posts
                                            .SingleOrDefault(p => p.PostId == PostId);
            _context.Posts.Remove(CurrentPost);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
    }
}