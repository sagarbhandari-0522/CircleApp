using CircleApp.Data.Models;

namespace CircleApp.Dtos
{
    public class SuggestedFriendsWithNumberOfFriendDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ProfilePictureUrl { get; set; }
        public int NumberOfFriends { get; set; }
    }
}
