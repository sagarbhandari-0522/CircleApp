using CircleApp.Data.Helpers;
using CircleApp.Services;
using CircleApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CircleApp.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {
        private readonly IUserSettingService _userSettingService;
        private readonly IFileService _fileService;
        public SettingController(IUserSettingService userSettingService, IFileService fileService)
        {
            _userSettingService = userSettingService;
            _fileService = fileService;
        }
        
        public IActionResult Index()
        {
            var currentUserId = 1;
            var user = _userSettingService.GetUserDetails(currentUserId);
            return View(user);
        }
        [HttpPost]
        public IActionResult UpdateProfilePicture(UpdateProfilePictureVM model)
        {
            var currentUserId = 1;
            var profilePictureUrl = _fileService.UploadFile(model.ProfilePictureImage, ImageType.ProfilePicture);
            _userSettingService.UpdateProfilePicture(currentUserId, profilePictureUrl);


            return RedirectToAction("Index");
        }
    }
}
