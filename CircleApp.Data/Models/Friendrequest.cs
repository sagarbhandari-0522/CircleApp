using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleApp.Data.Models
{
    public class Friendrequest
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int CreatedAt { get; set; }
        public int UpdatedAt { get; set; }
        public string Status { get; set; }

    }
}
