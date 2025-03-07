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
    }
}
