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
            return View();
        }
    }
}
