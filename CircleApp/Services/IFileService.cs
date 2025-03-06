using CircleApp.Data.Helpers;

namespace CircleApp.Services
{
    public interface IFileService
    {
        public string UploadFile(IFormFile image, ImageType imageType);
    }
}
