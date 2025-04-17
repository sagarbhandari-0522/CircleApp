using System.ComponentModel.DataAnnotations;

namespace CircleApp.ViewModels
{
    public class TogglePostLikeViewModel
    {
        [Required]
        public int postId { get; set; }
    }
}
