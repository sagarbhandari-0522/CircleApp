using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleApp.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
