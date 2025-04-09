using CircleApp.Data.Models;

namespace CircleApp.Services
{
    public interface IUserSettingService
    {
        public User GetUserDetails(int currentUserId);
        public User UpdateProfilePicture(int currentUserId, string imageUrl);
        public List<Post> GetUserPosts(int userId);
    }
}
