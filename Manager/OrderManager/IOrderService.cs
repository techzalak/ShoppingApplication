using ZalakProject.Models;

namespace ZalakProject.Manager.OrderManager
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersForBuyerAsync(string buyerId);
        Task<List<Order>> GetOrdersForSellerAsync(string sellerId);
    }
}