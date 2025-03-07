using CircleApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CircleApp.Controllers
{
    public class SettingController : Controller
    {
        private readonly IUserSettingService _userSetting;
        public SettingController(IUserSettingService userSetting)
        {
            _userSetting=userSetting;
        }
        
        public IActionResult Index()
        {
            var currentUserId = 1;
            var user = _userSetting.GetUserDetails(currentUserId);
            return View(user);
        }
    }
}
