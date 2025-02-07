using CircleApp.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace CircleApp.ViewModels
{
    public class PostCreateViewModel
    {

        public string? Content { get; set; }
        public IFormFile? Image { get; set; }
        public int NrOfReports { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
