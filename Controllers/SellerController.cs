using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ZalakProject.Manager.OrderManager;
using ZalakProject.Manager.ProductManager;
using ZalakProject.Models;
using ZalakProject.ViewModels;

namespace ZalakProject.Controllers
{
    [Authorize(Roles = "Seller")]
    public class SellerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        public SellerController(IProductService productService, UserManager<ApplicationUser> userManager, IOrderService orderService)
        {
            _productService = productService;
            _userManager = userManager;
            _orderService = orderService;
        }

        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            var products = await _productService.GetAllProductsBySellerId(user.Id);
            var orders = await _orderService.GetOrdersForSellerAsync(user.Id);

            

            var viewModel = new SellerDashboardViewModel
            {
                Products = products,
                TotalProducts = products.Count,
                TotalOrders = orders.Count,
                
            };

            return View(viewModel);
        }
    }
}
