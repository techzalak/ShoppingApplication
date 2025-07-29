using Microsoft.EntityFrameworkCore;
using ZalakProject.Data;
using ZalakProject.Models;

namespace ZalakProject.Manager.OrderManager
{
    public class OrderService : IOrderService
    {

        private readonly ApplicationDbContext _context;
        private readonly DbSet<Order> _orderDao;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
            _orderDao = _context.Orders;
        }

        public async Task<List<Order>> GetOrdersForSellerAsync(string sellerId)
        {
            return await _orderDao.AsNoTracking().Where(q => q.SellerId == sellerId).ToListAsync();
        }

        public async Task<List<Order>> GetOrdersForBuyerAsync(string buyerId)
        {
            return await _orderDao.AsNoTracking().Where(q => q.BuyerId == buyerId).ToListAsync();
        }
    }
}
