using CircleApp.Data;
using CircleApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CircleApp.Services
{
    public class UserSettingService : IUserSettingService
    {
        private readonly ApplicationDbContext _context;
        public UserSettingService(ApplicationDbContext context)
        {
            _context = context;
        }
        public User GetUserDetails(int currentUserId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == currentUserId);
            return user;
        }
        public User UpdateProfilePicture(int currentUserId, string imageUrl)
        {
            var user = GetUserDetails(currentUserId);
            if (user != null)
            {
                user.ProfilePictureUrl = imageUrl;
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            return user;
        }
        public List<Post> GetUserPosts(int userId)
        {
            var userPosts = _context.Posts.
                Where(p => (p.UserId == userId) && (p.Reports.Count < 5) && (!p.Reports.Any(r => r.UserId == userId)) && (!p.IsDeleted))
                .Include(p => p.User)
                .Include(p => p.Likes)
                .Include(p => p.Favorites)
                .Include(p => p.Reports)
                .Include(p => p.Comments).ThenInclude(c => c.User)
                .OrderByDescending(p => p.UpdatedAt)
                .ToList();
            return (userPosts);




        }

    }
}
