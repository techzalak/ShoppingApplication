using ZalakProject.ViewModels;

namespace ZalakProject.Manager.Buyer
{
    public interface IBuyerService
    {
        Task<PaginatedResult<BuyerProductMoreDetails>> GetProductList(int pageNumber, int? categoryId);
        Task<BuyerProductMoreDetails> ViewMoreDetails(string productId);
    }
}
