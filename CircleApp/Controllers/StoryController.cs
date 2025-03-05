using CircleApp.Data;
using CircleApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CircleApp.ViewModels;
using CircleApp.Services;


namespace CircleApp.Controllers
{
    public class StoryController : Controller
    {
        private readonly ILogger<StoryController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStoryService _storyService;

        public StoryController(ILogger<StoryController> logger, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment,IStoryService storyService)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _storyService = storyService;
        }

        [HttpPost]
        public IActionResult Create(StoryCreateVM model)
        {
            var currentUserId = 1;
            var story = new Story
            {
                ImageUrl = null,
                CreatedAt = DateTime.UtcNow,
                UserId = currentUserId,
            };
            _storyService.CreateStory(story,model.Image);
            return RedirectToAction("Index", "Home");
        }
    }
}
