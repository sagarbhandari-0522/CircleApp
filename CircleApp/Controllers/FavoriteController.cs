using CircleApp.Controllers.Base;
using CircleApp.Data;
using CircleApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CircleApp.Controllers
{
    [Authorize]
    public class FavoriteController : BaseController
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
            var currentUserId = GetUserId();
            if (currentUserId == null) return RedirectToLogin();
            var favoritePosts = _favoriteService.GetAllFavoritePost(currentUserId.Value);
            ViewBag.ShowAllComments = false;
            return View(favoritePosts);
        }
    }
}
