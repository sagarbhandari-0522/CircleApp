using CircleApp.Data;
using CircleApp.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CircleApp.Services
{
    public class StoryService : IStoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public StoryService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public Story CreateStory(Story story)
        {

            _context.Stories.Add(story);
            _context.SaveChanges();
            return story;
        }

        public List<Story> GetAllStories()
        {
            var stories = _context.Stories.AsNoTracking()
             .Include(s => s.User)
              .Where(s => s.CreatedAt >= DateTime.UtcNow.AddHours(-24))
              .ToList();
            return stories;
        }
    }
}
