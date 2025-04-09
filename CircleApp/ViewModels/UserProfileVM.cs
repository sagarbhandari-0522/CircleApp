using CircleApp.Data.Models;

namespace CircleApp.ViewModels

{
    public class UserProfileVM
    {
        public User User { get; set; }
        public List<Post> Posts { get; set; }
    }
}
