using CircleApp.Data;
using CircleApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CircleApp.ViewModels;


namespace CircleApp.Controllers
{
    public class StoryController : Controller
    {
        private readonly ILogger<StoryController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StoryController(ILogger<StoryController> logger, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }
    
        [HttpPost]
        public IActionResult Create(StoryCreateVM model)
        {
            string uniqueFileName = null;
            var currentUserId = 1;
            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "stories");
                Directory.CreateDirectory(uploadsFolder);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                model.Image.CopyTo(new FileStream(filePath, FileMode.Create));

                var story = new Story
                {
                    ImageUrl = uniqueFileName,
                    CreatedAt = DateTime.UtcNow,
                    UserId = currentUserId,
                };
                _context.Stories.Add(story);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
