using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ZalakProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Address { get; set; }

        [StringLength(50)]
        public string? City { get; set; }

        [StringLength(10)]
        public string? PostalCode { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? ProfileImageData { get; set; }  // Base64 string
        public string? ProfileImageName { get; set; }

        // Navigation properties
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
