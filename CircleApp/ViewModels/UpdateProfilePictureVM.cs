namespace CircleApp.ViewModels
{
    public class UpdateProfilePictureVM
    {
        public int UserId { get; set; }
        public IFormFile ProfilePictureImage { get; set; }
    }
}
