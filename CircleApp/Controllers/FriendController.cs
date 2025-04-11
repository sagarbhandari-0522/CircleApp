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
            var receivedFriendRequest = await _friendshipService.GetReceivedFriendRequestAsync(currentUserId.Value);
            var result = await _friendshipService.GetFriendsAsync(currentUserId.Value);
            if(!result.Success) return RedirectToAction("Index");
            var friendsVm = new FriendsVM()
            {
                SentFriendRequest = sentFriendRequest,
                ReceivedFriendRequest = receivedFriendRequest,
                Friends = result.Friends

            };
            return View(friendsVm);
        }
        [HttpPost]
        public async Task<IActionResult> SendFriendRequest(int receiverId)
        {
            if (receiverId == 0) return RedirectToAction("Index");
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
        [HttpPost]
        public async Task<IActionResult> CancleFriendRequest(int requestId)
        {
            if (requestId == 0) return RedirectToAction("Index");
            var result = await _friendshipService.CancelRequest(requestId);
            if(result.Success)
            {
                TempData["Message"] = "Request has been Successfully Cancled";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = result.Errors[0];
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AcceptFriendRequest(int requestId)
        {
            if (requestId == 0) return RedirectToAction("index");
            var result = await _friendshipService.AcceptRequest(requestId);
            if(result.Success)
            {
                TempData["Message"] = "Request has been successfully Accepted";
                return RedirectToAction("Index");
            }  
            else
            {
                TempData["Error"] = result.Errors[0];
                return RedirectToAction("Index");
            }
        }

    }
}
