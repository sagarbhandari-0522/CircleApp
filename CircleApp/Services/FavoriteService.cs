using CircleApp.Data;
using CircleApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CircleApp.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly ApplicationDbContext _context;
        public FavoriteService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Post> GetAllFavoritePost(int loggedInUserId)
        {
            var allFavoritePost = _context.Favorites
                .Where(f => f.UserId == loggedInUserId)
                 .Where(f => !f.Post.IsDeleted && f.Post.Reports.Count < 5)
                 .Include(f => f.Post)
                    .ThenInclude(p => p.Comments)
                        .ThenInclude(c => c.User)
                  .Include(f => f.Post)
                    .ThenInclude(p => p.Likes)
                  .Include(f=>f.Post)
                    .ThenInclude(p=>p.User)
                 .Include(f=>f.Post)
                    .ThenInclude(p=>p.Favorites)
                  .Select(f=>f.Post)
                  .ToList();
            return allFavoritePost;

            // !f.Post.IsDeleted && f.Post.Reports.Count<5)
            //.Select(f => f.Post)
            //.Include(p => p.Comments)
            //    .ThenInclude(c=>c.User)
            //.Include(p => p.Likes);
        }
    }
}
