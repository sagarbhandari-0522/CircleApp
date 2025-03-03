using CircleApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace CircleApp.Components
{
    public class HashtagsViewComponent :ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public HashtagsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var lastWeek = DateTime.UtcNow.AddDays(-7);
            var tags = _context.Hashtags.Where(t => t.CreatedAt >= lastWeek).OrderByDescending(t=>t.Count).Take(3).ToList();
            return View(tags);
        }
    }
}
