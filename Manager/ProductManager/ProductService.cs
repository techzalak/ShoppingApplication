using Microsoft.EntityFrameworkCore;
using ZalakProject.Data;
using ZalakProject.Models;
using ZalakProject.ViewModels;
namespace ZalakProject.Manager.ProductManager
{
	public class ProductService : IProductService
	{
		private readonly ApplicationDbContext _context;
		private readonly DbSet<Product> _productDao;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public ProductService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_productDao = context.Products;
			_webHostEnvironment = webHostEnvironment;
		}

		public async Task<ProductEditViewModel> GetProductEditModel(string productId)
		{
			Product product = await FindProductByIdAsync(productId);
			ProductEditViewModel productEditViewModel = new ProductEditViewModel
			{
				ProductId = productId,
				CategoryId = product.CategoryId,
				Description = product.Description,
				Name = product.Name,
				Price = product.Price,
				StockQuantity = product.StockQuantity,
			};

			if (product.ProductImages.Any())
			{
				var imageFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "product-images");

				foreach (var image in product.ProductImages)
				{
					productEditViewModel.ExistingImages.Add(new ProductImageViewModel
					{
						Alt = image.Alt,
						FileData = image.FileData,
						FileName = image.FileName,
						Id = image.Id,
						IsPrimary = image.IsPrimary,
					});
				}
			}

			return productEditViewModel;
		}

		public async Task<Product> FindProductByIdAsync(string productId)
		{
			return await _productDao.AsNoTracking().Where(p => p.Id == productId).Include(p => p.ProductImages).FirstOrDefaultAsync();
		}

		public async Task<List<ProductListViewModel>> GetAllProductsBySellerId(string sellerId)
		{
			List<ProductListViewModel> dtolist = new List<ProductListViewModel>();
			var dbResult = await _productDao.AsNoTracking().Include(q => q.Category).Where(q => q.SellerId == sellerId).ToListAsync();
			dtolist = dbResult.Select(Create).ToList();
			return dtolist;
		}

		public async Task<bool> CreateProductAsync(ProductCreateViewModel viewModel)
		{
			try
			{
				if (viewModel == null)
				{
					return false;
				}
				else if (viewModel.Price <= 0.00
					|| viewModel.Images == null
					|| viewModel.Images.Count > 3
					|| viewModel.StockQuantity <= 0
					|| viewModel.CategoryId <= 0)
				{
					return false;
				}
				var imageFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "product-images");


				if (!Directory.Exists(imageFolderPath))
					Directory.CreateDirectory(imageFolderPath);

				Product model = Create(viewModel);

				foreach (var image in viewModel.Images)
				{
					if (image != null && image.Length > 0)
					{
						using (var memoryStream = new MemoryStream())
						{
							await image.CopyToAsync(memoryStream);
							var fileBytes = memoryStream.ToArray();
							var base64String = Convert.ToBase64String(fileBytes);

							model.ProductImages.Add(new ProductImage
							{
								ProductId = model.Id,
								FileName = Path.GetFileName(image.FileName),
								FileData = base64String,
								Alt = Path.GetFileNameWithoutExtension(image.FileName),
								IsPrimary = false // or set logic for primary image
							});
						}
					}
				}
				_productDao.Add(model);
				await _context.ProductImages.AddRangeAsync(model.ProductImages);
				await _context.SaveChangesAsync();

				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public async Task<bool> UpdateProductAsync(ProductEditViewModel viewModel)
		{
			var product = await _productDao.AsTracking().Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == viewModel.ProductId);

			if (product == null)
			{
				return false;
			}
			else if (viewModel.Price <= 0.00
					|| viewModel.ExistingImages.Count > 3
					|| viewModel.StockQuantity <= 0
					|| viewModel.CategoryId <= 0)
			{
				return false;
			}

			product.Name = viewModel.Name;
			product.Description = viewModel.Description;
			product.Price = viewModel.Price;
			product.CategoryId = viewModel.CategoryId;
			product.StockQuantity = viewModel.StockQuantity;
			product.UpdatedAt = DateTime.Now;

			if (viewModel.NewImages != null && viewModel.NewImages.Any())
			{
				foreach (var image in viewModel.NewImages)
				{
					if (image != null && image.Length > 0)
					{
						using (var memoryStream = new MemoryStream())
						{
							await image.CopyToAsync(memoryStream);
							var fileBytes = memoryStream.ToArray();
							var base64String = Convert.ToBase64String(fileBytes);

							product.ProductImages.Add(new ProductImage
							{
								ProductId = product.Id,
								FileName = Path.GetFileName(image.FileName),
								FileData = base64String,
								Alt = Path.GetFileNameWithoutExtension(image.FileName),
								IsPrimary = false // or set logic for primary image
							});
						}
					}
				}
			}
			_context.Products.Update(product);
			_context.ProductImages.UpdateRange(product.ProductImages);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task DeleteAsync(string productId)
		{
			Product model = await _productDao.FindAsync(productId);
			_productDao.Remove(model);
			await _context.SaveChangesAsync();
		}

		private ProductListViewModel Create(Product product)
		{
			return new ProductListViewModel
			{
				CategoryId = product.CategoryId,
				CategoryName = product.Category.Name,
				Description = product.Description,
				Price = product.Price,
				ProductId = product.Id,
				ProductName = product.Name,
				ProductImages = new(),
				SellerId = product.SellerId,
				StockQuantity = product.StockQuantity,
			};
		}

		private Product Create(ProductCreateViewModel viewModel)
		{
			return new Product
			{
				Name = viewModel.Name,
				CategoryId = viewModel.CategoryId,
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				Price = viewModel.Price,
				Description = viewModel.Description,
				SellerId = viewModel.SellerId,
				StockQuantity = viewModel.StockQuantity,
			};
		}
	}
}
