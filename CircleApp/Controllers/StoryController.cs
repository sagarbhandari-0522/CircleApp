﻿using CircleApp.Data;
using CircleApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CircleApp.ViewModels;
using CircleApp.Services;
using CircleApp.Data.Helpers;
using Microsoft.AspNetCore.Authorization;
using CircleApp.Controllers.Base;


namespace CircleApp.Controllers
{
    [Authorize]
    public class StoryController : BaseController
    {
        private readonly ILogger<StoryController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStoryService _storyService;
        private readonly IFileService _fileService;

        public StoryController(ILogger<StoryController> logger, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment,IStoryService storyService, IFileService fileService)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _storyService = storyService;
            _fileService = fileService;
        }

        [HttpPost]
        public IActionResult Create(StoryCreateVM model)
        {
            int? currentUserId = GetUserId();
            if (currentUserId == null) return RedirectToLogin();
            var story = new Story
            {
                ImageUrl = _fileService.UploadFile(model.Image, ImageType.StoryImage),
                CreatedAt = DateTime.UtcNow,
                UserId = currentUserId.Value,
            };
            _storyService.CreateStory(story);
            return RedirectToAction("Index", "Home");
        }
    }
}
