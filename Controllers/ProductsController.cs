using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ZalakProject.Manager.CategoryManager;
using ZalakProject.Manager.ProductManager;
using ZalakProject.Models;
using ZalakProject.ViewModels;

namespace ZalakProject.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProductService _productService;
        public ProductsController(ICategoryService categoryService, UserManager<ApplicationUser> userManager, IProductService productService)
        {
            _categoryService = categoryService;
            _userManager = userManager;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            SelectList selectListItems = new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Name");
            ViewBag.Categories = selectListItems;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            model.SellerId = user.Id;
            
            var result  = await _productService.CreateProductAsync(model);

            if (result)
                return RedirectToAction("Dashboard", "Seller");
            else
            {
                ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
                return View(model);

            }
        }

        public async Task<IActionResult> Delete(string productId)
        {
           await _productService.DeleteAsync(productId);
            return RedirectToAction("Dashboard", "Seller");
        }

        public async Task<IActionResult> Edit(string productId)
        {
            var product = await _productService.GetProductEditModel(productId);

            if (product == null)
                return NotFound();


            SelectList selectListItems = new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Name");
            ViewBag.Categories = selectListItems;

            return View(product);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ProductEditViewModel viewModel)
        {
            var product = await _productService.UpdateProductAsync(viewModel);


            if (product)
                return RedirectToAction("Dashboard", "Seller");
            else
            {
                ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Name", viewModel.CategoryId);
                return View(viewModel);
            }
        }
    }
}
