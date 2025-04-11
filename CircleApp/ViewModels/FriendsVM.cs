using CircleApp.Data.Models;

namespace CircleApp.ViewModels
{
    public class FriendsVM
    {
        public List<Friendrequest> SentFriendRequest { get; set; }
        public List<Friendrequest> ReceivedFriendRequest { get; set; }
        public List<User> Friends { get; set; }
    }
}
