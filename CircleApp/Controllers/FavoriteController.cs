using CircleApp.Data;
using CircleApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CircleApp.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFavoriteService _favoriteService;
        public FavoriteController(ApplicationDbContext context, IFavoriteService favoriteService)
        {
            _context = context;
            _favoriteService = favoriteService;
        }
        public IActionResult Index()
        {
            var loggedInUser = 1;
            var favoritePosts = _favoriteService.GetAllFavoritePost(loggedInUser);
            ViewBag.ShowAllComments = false;
            return View(favoritePosts);
        }
    }
}
