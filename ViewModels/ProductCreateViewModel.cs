using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZalakProject.ViewModels
{
    public class ProductCreateViewModel
    {
        [BindNever]
        public string SellerId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        
        public string? Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 999999.99)]
        public double Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [Range(0, 999999)]  
        public int StockQuantity { get; set; }
        [Required]
        public List<IFormFile> Images { get; set; }
    }
}
