using Microsoft.AspNetCore.Mvc.Rendering;
using ZalakProject.Models;

namespace ZalakProject.Manager.CategoryManager
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesAsync();
    }
}