using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZalakProject.Data;
using ZalakProject.Manager.Buyer;
using ZalakProject.Manager.CategoryManager;
using ZalakProject.Manager.ReviewManager;
using ZalakProject.Models;
using ZalakProject.ViewModels;

namespace ZalakProject.Controllers
{
    [Authorize(Roles ="Buyer")]
    public class BuyerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBuyerService _buyerService;
        private readonly IReviewService _reviewService;

        private readonly ICategoryService _categoryService;
        private readonly UserManager<ApplicationUser> _userManager;
        public BuyerController(ApplicationDbContext context, IBuyerService buyerService, IReviewService reviewService, UserManager<ApplicationUser> userManager, ICategoryService categoryService)
        {
            _context = context;
            _buyerService = buyerService;
            _reviewService = reviewService;
            _userManager = userManager;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Dashboard(int pageNumber = 1, int? categoryId = null)
        {
            var products = await _buyerService.GetProductList(pageNumber,categoryId);
            var categories = new SelectList ( await _categoryService.GetAllCategoriesAsync(), "Id", "Name" );
            ViewBag.Categories = categories;
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> ViewDetails(string productId)
        {
            var viewModel = await _buyerService.ViewMoreDetails(productId);
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> PostReview(ReviewViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            await _reviewService.AddReview(model, user.Id);
            return RedirectToAction("ViewDetails", new { productId = model.ProductId });
        }
        //[HttpGet]
        //public async Task<IActionResult> FilterByCategory(string? categoryId)
        //{
        //    var viewModel = await _buyerService.GetProductListFilteredByCategory(categoryId); // You define this service method
        //    viewModel.Categories = await _context.Categories.ToListAsync(); // or from service
        //    viewModel.CategoryId = categoryId; // Set selected back to persist selection in UI
        //    return View("ProductList", viewModel); // Use your actual view name
        //}
        public IActionResult Index()
        {
            return View();
        }
    }
}
