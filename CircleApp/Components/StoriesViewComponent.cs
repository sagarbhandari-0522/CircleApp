using CircleApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace CircleApp.Components
{
    public class StoriesViewComponent:ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public StoriesViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
       public async Task< IViewComponentResult> InvokeAsync()
        {
            var stories = _context.Stories.AsNoTracking()
               .Include(s=>s.User)
                .Where(s=>s.CreatedAt>=DateTime.UtcNow.AddHours(-24))
                .ToList();
            return View(stories);
        }
    }
}
