using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleApp.Data.Models
{
    public class User :IdentityUser<int>
    { 
        [Required]
        public string FullName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? Bio { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Report> Reports { get; set; } = new List<Report>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public ICollection<Story> Stories { get; set; } = new List<Story>();
        public ICollection<Friendship> SentFriendRequest { get; set; } = new List<Friendship>();
        public ICollection<Friendship> ReceivedFriendRequest { get; set; } = new List<Friendship>();

    }

}
