using CircleApp.Data.Models;

namespace CircleApp.Services
{
    public interface IFavoriteService
    {
        public List<Post> GetAllFavoritePost(int loggedInUserId);
    }
}
