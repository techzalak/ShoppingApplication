using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Drawing.Printing;
using ZalakProject.Data;
using ZalakProject.Manager.CategoryManager;
using ZalakProject.Manager.ProductManager;
using ZalakProject.Models;
using ZalakProject.ViewModels;

namespace ZalakProject.Manager.Buyer
{
    public class BuyerService:IBuyerService
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Product> _productsDao;

        public BuyerService(ApplicationDbContext context)
        {
            _context = context;
            _productsDao = _context.Products;
        }

        public async Task<PaginatedResult<BuyerProductMoreDetails>> GetProductList(int pageNumber, int? categoryId)
        {
            int pageSize = 9;
            var query = _productsDao.Include(p => p.ProductImages)
                       .AsQueryable();
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            int totalRecords = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var product = await query.OrderByDescending(p => p.CreatedAt).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            List<BuyerProductMoreDetails> buyerProductViewModels = new List<BuyerProductMoreDetails>();

            if (product.Any())
            {
                foreach (var model in product)
                {
                    BuyerProductMoreDetails viewModel = new BuyerProductMoreDetails();
                    viewModel.ProductId = model.Id;
                    viewModel.ProductName = model.Name;
                    viewModel.Price = model.Price;
                    if (model.ProductImages.Any())
                    {
                        foreach (var image in model.ProductImages)
                        {
                            viewModel.ProductImages.Add(new ProductImageViewModel
                            {
                                Alt = image.Alt,
                                FileData = image.FileData,
                                FileName = image.FileName,
                                Id = image.Id
                            });
                        }
                    }
                    buyerProductViewModels.Add(viewModel);

                }
            }




            return new PaginatedResult<BuyerProductMoreDetails>
            {
                Items = buyerProductViewModels,
                CurrentPage = pageNumber,
                TotalPages = totalPages
            };
        }

        public async Task<BuyerProductMoreDetails> ViewMoreDetails(string productId)
		{
			var productItem = await _productsDao.Include(r => r.Reviews).ThenInclude(u => u.User)
				.Include(p => p.ProductImages)
				.FirstOrDefaultAsync(p => p.Id == productId);

			List<ProductImageViewModel> productImageViewModel = new List<ProductImageViewModel>();
			if (productItem.ProductImages.Any())
			{
				foreach (var image in productItem.ProductImages)
				{
					productImageViewModel.Add(new ProductImageViewModel
					{

						Alt = image.Alt,
						FileData = image.FileData,
						FileName = image.FileName,
						Id = image.Id
					});
				}
			}
			return new BuyerProductMoreDetails
			{
				reviews = productItem.Reviews.ToList(),
				StockQuantity = productItem.StockQuantity,
				ProductName = productItem.Name,
				Description = productItem.Description,
				Price = productItem.Price,
				ProductImages = productImageViewModel
            };
		}
	}
}
