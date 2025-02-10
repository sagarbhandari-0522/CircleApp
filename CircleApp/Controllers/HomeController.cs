using CircleApp.Data;
using Humanizer;
using CircleApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using CircleApp.ViewModels;
using Microsoft.Extensions.Hosting;

namespace CircleApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {

            var allPosts = _context.Posts
                .Include(n => n.User)
                .Include(n=>n.Likes)
                .OrderByDescending(n => n.UpdatedAt)
                .ToList();
            return View(allPosts);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PostCreateViewModel model)
        {

            string uniqueFileName = null;
            int currentUserId = 1;
            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "uploads");
                Directory.CreateDirectory(uploadsFolder);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                model.Image.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            Post post = new Post
            {
                Content = model.Content,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ImageUrl = uniqueFileName,
                User = _context.Users.FirstOrDefault(u=>u.Id==currentUserId),
                NrOfReports = 0
            };



            if (ModelState.IsValid)
            {
                _context.Posts.Add(post);
                _context.SaveChanges();

            }

            else
            {
                foreach (var state in ModelState.Values)
                {
                    foreach (var error in state.Errors)
                    {
                        Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                    }
                }
            }



            return RedirectToAction("Index");


        }
        [HttpPost]
        public IActionResult TogglePostLike(TogglePostLikeViewModel model)
        {
            var currentUserId = 1;
            var likeToRemove = _context.Likes.FirstOrDefault(l => l.UserId == currentUserId && l.PostId == model.postId);
            if(likeToRemove!=null)
            {
                _context.Likes.Remove(likeToRemove);
                _context.SaveChanges();
            }
            else
            {
                var like = new Like()
                {
                    Post = _context.Posts.FirstOrDefault(p => p.Id == model.postId),
                    User = _context.Users.FirstOrDefault(u => u.Id == currentUserId),
                    CreatedAt = DateTime.UtcNow

                };
                _context.Likes.Add(like);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult PostComment(PostCommentVM model)
        {
            var currentUserId = 1;
            var comment = new Comment
            {
                UserId = currentUserId,
                PostId = model.PostId,
                Content = model.Content,
                CreatedAt=DateTime.UtcNow,
                UpdatedAt=DateTime.UtcNow

            };
            _context.Comments.Add(comment);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}
