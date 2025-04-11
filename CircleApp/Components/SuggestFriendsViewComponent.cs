using CircleApp.Data;
using CircleApp.Data.Models;
using CircleApp.Services;
using CircleApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CircleApp.Components
{
    public class SuggestFriendsViewComponent :ViewComponent
    {
        private readonly IFriendshipService _friendshipService;
        private readonly UserManager<User> _userManager;
        public SuggestFriendsViewComponent(IFriendshipService friendshipService, UserManager<User> userManager)
        {
            _friendshipService = friendshipService;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loggedInUserId =  _userManager.GetUserId(HttpContext.User);
            if(loggedInUserId==null)
            {
                throw new Exception("User must be logged in");
            }
            var suggestedFriendsWithNumberOfFriendDto = await _friendshipService.GetSuggestedFriends(Int32.Parse(loggedInUserId));
            var suggestedFriendWithNumberOfFriendVM = suggestedFriendsWithNumberOfFriendDto.Select(f => new SuggestedFriendWithNumberOfFriendVM()
            {
                Id = f.Id,
                FullName = f.FullName,
                ProfilePictureUrl = f.ProfilePictureUrl,
                NumberOfFriend = f.NumberOfFriends
            }).ToList();
            return View(suggestedFriendWithNumberOfFriendVM);
        }
    }
}
