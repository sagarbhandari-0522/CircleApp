using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleApp.Data.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }
        public int? PostId { get; set; }
        public User? User { get; set; }
        public Post? Post { get; set; }


    }
}
