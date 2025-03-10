using CircleApp.Data;
using CircleApp.Data.Models;

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
            if(user!=null)
            {
                user.ProfilePictureUrl = imageUrl;
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            return user;
        }

    }
}
