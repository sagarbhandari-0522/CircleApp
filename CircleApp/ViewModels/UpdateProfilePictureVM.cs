using System.ComponentModel.DataAnnotations;

namespace CircleApp.ViewModels
{
    public class UpdateProfilePictureVM
    {
        public int UserId { get; set; }
        [Required]
        public IFormFile ProfilePictureImage { get; set; }
    }
}
