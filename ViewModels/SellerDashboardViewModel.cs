namespace ZalakProject.ViewModels
{
    public class SellerDashboardViewModel
    {
        public List<ProductListViewModel> Products { get; set; }
        public int TotalProducts { get; set; }
        public int TotalOrders { get; set; }
    }
}
