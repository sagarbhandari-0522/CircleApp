using CircleApp.Data;
using Humanizer;
using CircleApp.Data.Models;
using CircleApp.Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using CircleApp.ViewModels;
using Microsoft.Extensions.Hosting;
using CircleApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace CircleApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPostsService _postService;
        private readonly IHashtagService _hashTagService;
        private readonly IFileService _fileService;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IPostsService postService, IHashtagService hashTagService, IFileService fileService)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _postService = postService;
            _hashTagService = hashTagService;
            _fileService = fileService;

        }
       
        public IActionResult Index()
        {
            var currentUserId = 1;
            var allPosts = _postService.GetAllPosts(currentUserId);
            ViewBag.ShowAllComments = false;
            return View(allPosts);
        }
        public IActionResult Details( int postId)
        {
            var post = _postService.GetPostDetailsById(postId);
            ViewBag.ShowAllComments = true;
            return View(post);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PostCreateViewModel model )
        {
            int currentUserId = 1;
            Post post = new Post
            {
                Content = model.Content,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ImageUrl = _fileService.UploadFile(model.Image, ImageType.PostImage),
                User = _context.Users.FirstOrDefault(u => u.Id == currentUserId),
                NrOfReports = 0
            };
            _postService.CreatePost(post);
            _hashTagService.ProcessHashtagsForNewPost(post.Content);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult TogglePostLike(TogglePostLikeViewModel model)
        {
            var currentUserId = 1;
            _postService.TooglePostLike(model.postId, currentUserId);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult TogglePostVisibility(TogglePostVisibilityVM model)
        {
            var currentUserId = 1;
            _postService.TogglePostVisibility(model.PostId, currentUserId);
           
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult PostComment(PostCommentVM model)
        {
            var currentUserId = 1;
            var comment = new Comment
            {
                UserId = currentUserId,
                PostId = model.PostId,
                Content = model.Content,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                User = _context.Users.FirstOrDefault(u => u.Id == currentUserId),
                Post = _context.Posts.FirstOrDefault(p => p.Id == model.PostId)

            };
            _postService.AddPostComment(comment);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult RemovePostComment(int commentId)
        {
            _postService.RemovePostComment(commentId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult TogglePostFavorite(TogglePostFavoriteVM model)
        {
            var currentUserId = 1;
            _postService.TooglePostFavorite(model.PostId, currentUserId);
          
            return RedirectToAction("Index");
        }

        public IActionResult AddPostReport(PostReportVM model)
        {
            var currentUserId = 1;
            _postService.ReportPost(model.PostId, currentUserId);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult RemovePost(PostDeleteVM model)
        {
            var post=_postService.RemovePost(model.PostId);
            _hashTagService.ProcessHashtagsForRemovePost(post.Content);
            return RedirectToAction("Index");
        }
    }
}
