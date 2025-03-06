
using CircleApp.Data;
using CircleApp.Data.Helpers;
using CircleApp.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace CircleApp.Services
{
    public class FileService : IFileService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public string UploadFile(IFormFile image, ImageType imageType)
        {
            if (image != null)
            {
                string folderPath = imageType switch
                {
                    ImageType.ProfilePicture => "uploads/profile_pictures/",
                    ImageType.PostImage => "uploads/post_images/",
                    ImageType.StoryImage => "uploads/story_images/",
                    ImageType.CoverImage => "uploads/cover_images",
                    _ => throw new ArgumentException("Invalid Image type")
                };

                return SaveFile(image,folderPath);
            }
            return "";
        }
        public string SaveFile(IFormFile image, string folderPath)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", folderPath);
            Directory.CreateDirectory(uploadsFolder);
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            image.CopyTo(new FileStream(filePath, FileMode.Create));
            return uniqueFileName;
        }
    }

}
