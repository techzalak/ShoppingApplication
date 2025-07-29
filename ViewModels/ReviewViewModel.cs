using System.ComponentModel.DataAnnotations;

namespace ZalakProject.ViewModels
{
    public class ReviewViewModel
    {
        public string ProductId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(1000)]
        public string? Comment { get; set; }
    }
}
