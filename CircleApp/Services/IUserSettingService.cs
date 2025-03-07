using CircleApp.Data.Models;

namespace CircleApp.Services
{
    public interface IUserSettingService
    {
        public User GetUserDetails(int currentUserId);
    }
}
