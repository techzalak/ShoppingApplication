using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZalakProject.Models;

namespace ZalakProject.ViewModels
{
    public class ProductEditViewModel
    {
        public string ProductId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, 999999.99)]
        public double Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [Range(0, 999999)]
        public int StockQuantity { get; set; }

        public List<Category> Categories { get; set; }

        public List<IFormFile> NewImages { get; set; } = new();
        public List<ProductImageViewModel> ExistingImages { get; set; } = new();
    }
}
