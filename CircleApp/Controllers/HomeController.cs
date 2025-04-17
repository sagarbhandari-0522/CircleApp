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
using CircleApp.Controllers.Base;

namespace CircleApp.Controllers
{
    [Authorize]
    public class HomeController : BaseController
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
            int? currentUserId = GetUserId();
            if (currentUserId == null) return RedirectToLogin();
            var allPosts = _postService.GetAllPosts(currentUserId.Value);
            ViewBag.ShowAllComments = false;
            return View(allPosts);
        }
        public IActionResult Details(int postId)
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
        public IActionResult Create(PostCreateViewModel model)
        {
            int? currentUserId = GetUserId();
            if (currentUserId == null) return RedirectToLogin();
            Post post = new Post
            {
                Content = model.Content,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ImageUrl = _fileService.UploadFile(model.Image, ImageType.PostImage),
                User = _context.Users.FirstOrDefault(u => u.Id == currentUserId.Value),
                NrOfReports = 0
            };
            _postService.CreatePost(post);
            _hashTagService.ProcessHashtagsForNewPost(post.Content);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task< IActionResult> TogglePostLike(TogglePostLikeViewModel model)
        {

            if (!ModelState.IsValid) return BadRequest();
            int? currentUserId = GetUserId();
            if (currentUserId == null) return RedirectToLogin();
            var result = _postService.TooglePostLike(model.postId, currentUserId.Value);
            var likeCount = await  _postService.GetPostLikeCount(model.postId);
            return Json(new { success = result.Success, isLiked=result.isLiked, likeCount=likeCount });
        }
        [HttpPost]
        public IActionResult TogglePostVisibility(TogglePostVisibilityVM model)
        {
            int? currentUserId = GetUserId();
            if (currentUserId == null) return RedirectToLogin();
            _postService.TogglePostVisibility(model.PostId, currentUserId.Value);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult PostComment(PostCommentVM model)
        {
            int? currentUserId = GetUserId();
            if (currentUserId == null) return RedirectToLogin();
            var comment = new Comment
            {
                UserId = currentUserId.Value,
                PostId = model.PostId,
                Content = model.Content,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                User = _context.Users.FirstOrDefault(u => u.Id == currentUserId.Value),
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
        public async Task<IActionResult> TogglePostFavorite(TogglePostFavoriteVM model)
        {
            int? currentUserId = GetUserId();
            if (currentUserId == null) return RedirectToLogin();
            var result=await _postService.TooglePostFavoriteAsync(model.PostId, currentUserId.Value);
            var favoriteCount = await  _postService.GetPostFavoriteCount(model.PostId);
            return Json(new { success = result.success, isFavorite=result.isFavorite, favoriteCount=favoriteCount });
        }

        public IActionResult AddPostReport(PostReportVM model)
        {
            int? currentUserId = GetUserId();
            if (currentUserId == null) return RedirectToLogin();
            _postService.ReportPost(model.PostId, currentUserId.Value);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult RemovePost(PostDeleteVM model)
        {
            var post = _postService.RemovePost(model.PostId);
            _hashTagService.ProcessHashtagsForRemovePost(post.Content);
            return RedirectToAction("Index");
        }
    }
}
