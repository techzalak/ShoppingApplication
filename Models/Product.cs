using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZalakProject.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public double Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [Required]
        public string SellerId { get; set; } = string.Empty;

        // Navigation properties
        public virtual Category Category { get; set; } = null!;
        public virtual ApplicationUser Seller { get; set; } = null!;
        public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        [NotMapped]
        public double AverageRating => Reviews.Any() ? Reviews.Average(r => r.Rating) : 0;

        [NotMapped]
        public int ReviewCount => Reviews.Count;
    }
}
