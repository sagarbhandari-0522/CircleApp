using CircleApp.Controllers.Base;
using CircleApp.Services;
using CircleApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CircleApp.Controllers
{
    public class FriendController : BaseController
    {
        private readonly IFriendshipService _friendshipService;
        public FriendController(IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
        }
        public async Task<IActionResult> Index()
        {
            var currentUserId = GetUserId();
            if (!currentUserId.HasValue) return RedirectToLogin();
            var sentFriendRequest = await _friendshipService.GetSentFriendRequestAsync(currentUserId.Value);
            var friendsVm = new FriendsVM()
            {
                SentFriendRequest = sentFriendRequest
            };
            return View(friendsVm);
        }
        public async Task<IActionResult> SendFriendRequest(int receiverId)
        {
            var senderId = GetUserId();
            if (!senderId.HasValue) RedirectToLogin();
            var result = await _friendshipService.CreateFriendrequest(senderId.Value, receiverId);
            if (result.Success)
            {
                TempData["Message"] = "Request has been sent successfully";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Error"] = result.Errors[0];
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
