using CircleApp.Data.Models;
using CircleApp.Services;
using CircleApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CircleApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserSettingService _userService;
        private readonly UserManager<User> _userManager;
        public UserController(IUserSettingService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Details(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var userPosts = _userService.GetUserPosts(userId);
            var userProfileVm = new UserProfileVM
            {
                User = user,
                Posts = userPosts,
            };
            return View(userProfileVm);
        }
    }
}
