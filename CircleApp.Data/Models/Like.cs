using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleApp.Data.Models
{
    public class Like
    {
        [Required]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PostId { get; set; }
        public  required Post Post { get; set; }
        public int UserId { get; set; }
        public required User User { get; set; }
    }
}
