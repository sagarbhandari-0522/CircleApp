﻿using CircleApp.Data;
using CircleApp.Data.Helpers;
using CircleApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CircleApp.Services
{

    public class PostsService : IPostsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PostsService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public List<Post> GetAllPosts(int loggedInUserId)
        {
            var allPosts = _context.Posts
                .Where(n => (!n.IsPrivate || n.UserId == loggedInUserId) && (n.Reports.Count <= 5) && !n.Reports.Any(n => n.UserId == loggedInUserId) && !n.IsDeleted)
                .Include(n => n.User)
                .Include(n => n.Likes)
                .Include(n => n.Favorites)
                .Include(n => n.Reports)
                .Include(n => n.Comments).ThenInclude(n => n.User)
                .OrderByDescending(n => n.UpdatedAt)
                .ToList();
            return (allPosts);
        }
        public Post CreatePost(Post post)
        {
            try
            {
                _context.Posts.Add(post);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save post {ex.Message}");
            }
            return post;
        }
        public Post GetPostById(int postId)
        {
            Post post = _context.Posts
                .Include(n => n.User)
                .Include(n => n.Likes)
                .Include(n => n.Favorites)
                .Include(n => n.Reports)
                .Include(n => n.Comments).ThenInclude(n => n.User)
                .FirstOrDefault(p => p.Id == postId);
            return post;
        }
        public Post RemovePost(int postId)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == postId);
            if (post != null)
            {
                post.IsDeleted = true;
                _context.Posts.Update(post);
                _context.SaveChanges();
            }
            return post;
        }

        public void AddPostComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
        public async Task<bool> RemovePostCommentAsync(int commentId)
        {
            var comment = _context.Comments.FirstOrDefault(c => c.Id == commentId);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public void ReportPost(int postId, int userId)
        {
            var report = new Report()
            {
                UserId = userId,
                PostId = postId,
                CreatedAt = DateTime.UtcNow
            };
            _context.Reports.Add(report);
            _context.SaveChanges();
        }

        public async Task<(bool success, bool isFavorite)> TooglePostFavoriteAsync(int postId, int userId)
        {
            var favorite = await _context.Favorites.FirstOrDefaultAsync(f => f.UserId == userId && f.PostId == postId);
            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                await _context.SaveChangesAsync();
                return (true, false);
            }
            else
            {
                var newFavorite = new Favorite
                {
                    UserId = userId,
                    PostId = postId,
                    CreatedAt = DateTime.UtcNow
                };
                await _context.Favorites.AddAsync(newFavorite);
                await _context.SaveChangesAsync();
                return (true, true);
            }
        }

        public (bool Success, bool isLiked) TooglePostLike(int postId, int userId)
        {
            var likeToRemove = _context.Likes.FirstOrDefault(l => l.UserId == userId && l.PostId == postId);
            if (likeToRemove != null)
            {
                _context.Likes.Remove(likeToRemove);
                _context.SaveChanges();
                return (true, false);
            }
            else
            {
                var like = new Like()
                {
                    Post = _context.Posts.FirstOrDefault(p => p.Id == postId),
                    User = _context.Users.FirstOrDefault(u => u.Id == userId),
                    CreatedAt = DateTime.UtcNow

                };
                _context.Likes.Add(like);
                _context.SaveChanges();
                return (true, true);
            }
        }

        public void TogglePostVisibility(int postId, int userId)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == postId && p.UserId == userId);
            if (post != null)
            {
                post.IsPrivate = !post.IsPrivate;
                _context.Posts.Update(post);
                _context.SaveChanges();
            }
        }

        public Post GetPostDetailsById(int postId)
        {
            var post = _context.Posts
                 .Include(p => p.User)
                 .Include(p => p.Comments)
                     .ThenInclude(c => c.User)
                 .Include(p => p.Likes)
                 .Include(p => p.Favorites)
                 .FirstOrDefault(p => p.Id == postId);
            return post;
        }

        public async Task<int> GetPostLikeCount(int postId)
        {
            //var post = await (_context.Posts.FirstOrDefaultAsync(p => p.Id == postId).Include(p => p.Likes);
            var post = await _context.Posts.Include(p => p.Likes).FirstOrDefaultAsync(p => p.Id == postId);
            var likeCount = post?.Likes.Count ?? 0;
            return likeCount;
        }
        public async Task<int> GetPostFavoriteCount(int postId)
        {
            var post = await _context.Posts.Include(p => p.Favorites).FirstOrDefaultAsync(p => p.Id == postId);
            var favoriteCount = post?.Favorites?.Count() ?? 0;

            return favoriteCount;
        }
    }
}
