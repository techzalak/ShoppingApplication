using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZalakProject.Enums;

namespace ZalakProject.Models
{
    public class Order
    {
        [Key]
        public string Id { get; set; } = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

        // Buyer reference
        [Required]
        public string BuyerId { get; set; }

        [ForeignKey("BuyerId")]
        public ApplicationUser Buyer { get; set; }

        // Seller reference (optional, for convenience)
        [Required]
        public string SellerId { get; set; }

        [ForeignKey("SellerId")]
        public ApplicationUser Seller { get; set; }

        // Product reference
        [Required]
        public string ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public OrderStatusEnum Status { get; set; } = OrderStatusEnum.Pending;

        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}
