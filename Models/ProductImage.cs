using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZalakProject.Models
{
    public class ProductImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string ProductId { get; set; }

        [Required]
        [StringLength(500)]
        public string FileName { get; set; } 

        public string FileData { get; set; }

        [StringLength(200)]
        public string? Alt { get; set; }

        public bool IsPrimary { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual Product Product { get; set; } = null!;
    }
}
