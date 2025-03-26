using CircleApp.Data.Helpers;
using CircleApp.Services;
using CircleApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CircleApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using CircleApp.Data.Helpers;

namespace CircleApp.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {
        private readonly IUserSettingService _userSettingService;
        private readonly IFileService _fileService;
        private readonly UserManager<User> _userManager;
        private readonly IPasswordService _passwordService;
        public SettingController(IUserSettingService userSettingService, IFileService fileService, UserManager<User> userManager, IPasswordService passwordService)
        {
            _userSettingService = userSettingService;
            _fileService = fileService;
            _userManager = userManager;
            _passwordService = passwordService; 
        }

        public IActionResult Index()
        {
            TempData["ActiveTab"] = "updateProfile";
            var currentUserId = 2;

            var user = _userSettingService.GetUserDetails(currentUserId);
            var model = new SettingVM
            {
                User = user,
                UpdatePassword = new UpdatePasswordVM()

            };
            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateProfilePicture(UpdateProfilePictureVM model)
        {
            var currentUserId = 1;
            var profilePictureUrl = _fileService.UploadFile(model.ProfilePictureImage, ImageType.ProfilePicture);
            _userSettingService.UpdateProfilePicture(currentUserId, profilePictureUrl);


            return RedirectToAction("Index");
        }
        public async Task<IActionResult> UpdatePassword(SettingVM model)
        {
            TempData["ActiveTab"] = "updatePassword";// manipulating tab 
            try
            {
                ValidationHelper.RemoveUserFromModelStateKey(ModelState);
                
                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Invalid Request";
                    return View("Index", model);
                }
                var currentUserId = model.User?.Id;
                if (currentUserId == null)
                {
                    TempData["ErrorMessage"] = "User not found";
                    return RedirectToAction("Index");
                }
                // Call Password Service
                var (success, errors) = await _passwordService.UpdatePasswordAsync(currentUserId.ToString(),model.UpdatePassword.CurrentPassword,model.UpdatePassword.NewPassword, model.UpdatePassword.ConfirmPassword);
                if(success)
                {
                    TempData["SuccessMessage"] = "Password updated Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach(var error in errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
                return View("Index", model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Something went wrong";
                return RedirectToAction("Index");
            }
        }
        public IActionResult UpdateProfile()
        {
            return View("Index", "Home");
        }
    }
}
