using ZalakProject.Models;
using ZalakProject.ViewModels;

namespace ZalakProject.Manager.ProductManager
{
    public interface IProductService
    {
        Task<bool> CreateProductAsync(ProductCreateViewModel viewModel);
        Task DeleteAsync(string productId);
        Task<Product> FindProductByIdAsync(string productId);
        Task<List<ProductListViewModel>> GetAllProductsBySellerId(string sellerId);
        Task<ProductEditViewModel> GetProductEditModel(string productId);
        Task<bool> UpdateProductAsync(ProductEditViewModel viewModel);
    }
}
