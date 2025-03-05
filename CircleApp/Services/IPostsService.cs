using CircleApp.Data.Models;

namespace CircleApp.Services
{
    public interface IPostsService
    {
        List<Post> GetAllPosts(int loggedInUser);
        Post GetPostById(int postId);
        Post CreatePost(Post post, IFormFile image);
        Post RemovePost(int postId);
        void AddPostComment(Comment comment);
        void RemovePostComment(int commentId);
        void TooglePostLike(int postId,int userId);
        void TooglePostFavorite(int postId, int userId);
        void TogglePostVisibility(int postId, int userId);
        void ReportPost(int postId, int userId);




    }
}
