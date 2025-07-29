using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZalakProject.Models;

namespace ZalakProject.ViewModels
{
    public class CartViewModel
    {
        [Required]
        public string ProductId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string SellerId { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
