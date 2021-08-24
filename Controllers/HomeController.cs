using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bright_ideas_board.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Bright_ideas_board.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private MyContext _context;

        public HomeController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Register")]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "A user is already registered under that email");
                    return View("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);
                _context.Add(user);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("UserID", user.UserID);
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View("Index");
            }
        }

        public User loggedInUser()
        {
            int? loggedID = HttpContext.Session.GetInt32("UserID");
            User logged = _context.Users.FirstOrDefault(u => u.UserID == loggedID);
            return logged;
        }


        [HttpPost("ConfirmLogin")]
        public IActionResult ConfirmLogin(User userSubmission)
        {
            // Store user and verify email exists in db
            var userInDb = _context.Users.FirstOrDefault(u => u.Email == userSubmission.Email);
            if (userInDb == null)
            {
                // Console.WriteLine("**** No email found in DB ****");
                ModelState.AddModelError("Email", "There is no registered user under that email");
                return View("Index");
            }

            // Create password hasher and verify submitted password vs. database password
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            var result = Hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.Password);
            if (result == 0)
            {
                ModelState.AddModelError("Password", "Incorrect password. Please try again");
                return View("Index");
            }

            // If both above instances aren't triggered, redirect to login Dashboard page
            HttpContext.Session.SetInt32("UserID", userInDb.UserID);
            HttpContext.Session.SetString("UserName", userInDb.Name);
            return RedirectToAction("Dashboard");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            // if (loggedInUser() == null)
            // {
            //     return RedirectToAction("Index");
            // }
            int LoggedUserID = (int)HttpContext.Session.GetInt32("UserID");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.LoggedUser = LoggedUserID;

            ViewBag.AllIdeas = _context.Ideas
                .Include(c => c.Creator)
                .Include(like => like.IdeaLikes)
                .ThenInclude(like => like.Liker)
                .OrderByDescending(like => like.IdeaLikes.Count)
                .ToList();

            List<Idea> AllIdeas = _context.Ideas
                    .Include(c => c.Creator)
                    .Include(like => like.IdeaLikes)
                    .ThenInclude(like => like.Liker)
                    .OrderByDescending(like => like.IdeaLikes.Count)
                    .ToList();

            ViewBag.User = _context.Users
                .Include(user => user.LikedIdeas)
                .ThenInclude(like => like.LikedIdea)
                .FirstOrDefault(user => user.UserID == LoggedUserID);

            return View("Dashboard");
        }


        [HttpPost("/Idea/Create")]
        public IActionResult CreateIdea(Idea newIdea)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newIdea);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            else
            {
                int LoggedUserID = (int)HttpContext.Session.GetInt32("UserID");
                ViewBag.UserName = HttpContext.Session.GetString("UserName");
                ViewBag.LoggedUser = LoggedUserID;

                ViewBag.AllIdeas = _context.Ideas
                    .Include(c => c.Creator)
                    .Include(like => like.IdeaLikes)
                    .ThenInclude(like => like.Liker)
                    .ToList()
                    .OrderByDescending(like => like.TotalLikes);

                List<Idea> AllIdeas = _context.Ideas
                    .Include(c => c.Creator)
                    .Include(like => like.IdeaLikes)
                    .ThenInclude(like => like.Liker)
                    .OrderByDescending(like => like.TotalLikes)
                    .ToList();
                return View("Dashboard");
            }
        }

        [HttpGet("/Idea/{IdeaID}/Delete")]
        public IActionResult DeleteIdea(int IdeaID)
        {
            Idea retrievedIdea = _context.Ideas
                .SingleOrDefault(idea => idea.IdeaID == IdeaID);

            _context.Remove(retrievedIdea);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet("/Idea/{IdeaID}/Like")]
        public IActionResult AddLike(int IdeaID)
        {
            int? loggedID = HttpContext.Session.GetInt32("UserID");

            Idea retrievedIdea = _context.Ideas
                .SingleOrDefault(idea => idea.IdeaID == IdeaID);

            Like newLike = new Like();
            newLike.UserID = (int)loggedID;
            newLike.IdeaID = IdeaID;

            retrievedIdea.TotalLikes += 1;
            _context.Add(newLike);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");

        }

        [HttpGet("Idea/{IdeaID}/Unlike")]
        public IActionResult RemoveLike(int IdeaID)
        {
            int? loggedID = HttpContext.Session.GetInt32("UserID");

            Like retrievedLike = _context.Likes
                .SingleOrDefault(like => like.IdeaID == IdeaID && like.UserID == loggedID);
            
            _context.Remove(retrievedLike);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpGet("/Idea/{IdeaID}/View")]
        public IActionResult ViewIdea(int IdeaID)
        {
            int LoggedUserID = (int)HttpContext.Session.GetInt32("UserID");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.LoggedUser = LoggedUserID;

            Idea retrievedIdea = _context.Ideas
                .Include(c => c.Creator)
                .Include(like => like.IdeaLikes)
                .ThenInclude(like => like.Liker)
                .SingleOrDefault(idea => idea.IdeaID == IdeaID);

            return View(retrievedIdea);
        }

        [HttpGet("/User/{UserID}")]
        public IActionResult UserProfile(int UserID)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            User retrievedUser = _context.Users
                .Include(idea => idea.CreatedIdeas)
                .Include(user => user.LikedIdeas)
                .ThenInclude(like => like.LikedIdea)
                .SingleOrDefault(user => user.UserID == UserID);

            return View(retrievedUser);
        }
    }
}
