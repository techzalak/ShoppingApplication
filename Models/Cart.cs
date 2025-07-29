using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZalakProject.Models
{
    public class Cart
    {
        
        public string ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        
        public string BuyerId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser Buyer { get; set; }
        
        public string SellerId  { get; set; }
        public ApplicationUser Seller { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
