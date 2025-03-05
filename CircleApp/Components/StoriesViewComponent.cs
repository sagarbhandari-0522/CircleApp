using CircleApp.Data;
using CircleApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace CircleApp.Components
{
    public class StoriesViewComponent:ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly IStoryService _storyService;
        public StoriesViewComponent(ApplicationDbContext context, IStoryService storyService)
        {
            _context = context;
            _storyService = storyService;

        }
       public async Task< IViewComponentResult> InvokeAsync()
        {
            var stories=_storyService.GetAllStories();
            return View(stories);
        }
    }
}
