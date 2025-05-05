using CircleApp.Data.Models;

namespace CircleApp.Services
{
    public interface IPostsService
    {
        List<Post> GetAllPosts(int loggedInUser);
        Post GetPostById(int postId);
        Post GetPostDetailsById(int postId);
        Post CreatePost(Post post);
        Post RemovePost(int postId);

        void AddPostComment(Comment comment);
        Task<bool> RemovePostCommentAsync(int commentId);
        (bool Success, bool isLiked) TooglePostLike(int postId,int userId);
        Task<(bool success, bool isFavorite)> TooglePostFavoriteAsync(int postId, int userId);
        void TogglePostVisibility(int postId, int userId);
        void ReportPost(int postId, int userId);
        Task<int> GetPostLikeCount(int postId);
        Task<int> GetPostFavoriteCount(int postId);

    }
}
