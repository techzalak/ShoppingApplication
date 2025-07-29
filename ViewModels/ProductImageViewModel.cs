namespace ZalakProject.ViewModels
{
    public class ProductImageViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileData { get; set; } // Base64
        public string Alt { get; set; }
        public bool IsPrimary { get; set; }
    }
}
