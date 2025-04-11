using CircleApp.Controllers.Base;
using CircleApp.Services;
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
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SendFriendRequest(int receiverId)
        {
            var senderId = GetUserId();
            if (!senderId.HasValue) RedirectToAction();
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
