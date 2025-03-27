using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace CircleApp.ViewModels
{
    public class UpdatePasswordVM
    {
        [Required(ErrorMessage ="Current Password is required")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage ="New Password is required")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage="Confirm Password  is Required")]
        [Compare("NewPassword",ErrorMessage ="Confirm Password should match with New Password")]
        public string ConfirmPassword { get; set; }
    }
}
