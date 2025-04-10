using CircleApp.Data;
using CircleApp.Data.Models;
using CircleApp.Services;
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
            var suggestedFriends = await _friendshipService.GetSuggestedFriends(Int32.Parse(loggedInUserId));
            for(int i=0;i<1000;i++)
            {
                Console.Write("*");
            }
            Console.WriteLine(suggestedFriends);
            return View(suggestedFriends);
        }
    }
}
