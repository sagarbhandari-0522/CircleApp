using System.ComponentModel.DataAnnotations;

namespace CircleApp.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage="Firstname is required")]
        [MaxLength(50,ErrorMessage ="FirstName should be less than 50")]
        [MinLength(2,ErrorMessage ="Firstname should be more than 2 character")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Lastname can't be empty")]
        [MaxLength(50, ErrorMessage = "Lastname should be less than 50")]
        [MinLength(2, ErrorMessage = "Lastname should be more than 2 character")]
        public string LastName { get; set; }
        [Required(ErrorMessage ="Email should be required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password should be required")]
        public string Password { get; set; }
        [Compare("Password",ErrorMessage ="Confirmation password doesnot match to password")]
        public string ConfirmPassword { get; set; }
    }
}
