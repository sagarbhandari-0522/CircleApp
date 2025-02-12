using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleApp.Data.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public  DateTime CreatedAt {get;set;}
        public User? User;
        public Post? Post;
    }
}
