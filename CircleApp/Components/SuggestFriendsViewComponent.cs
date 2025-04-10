using CircleApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace CircleApp.Components
{
    public class SuggestFriendsViewComponent :ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
