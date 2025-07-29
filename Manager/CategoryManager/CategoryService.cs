using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZalakProject.Data;
using ZalakProject.Models;

namespace ZalakProject.Manager.CategoryManager
{
    public class CategoryService : ICategoryService
    {

        private readonly ApplicationDbContext _context;
        private readonly DbSet<Category> _dao;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
            _dao = context.Categories;
        }

        public Task<List<Category>> GetAllCategoriesAsync()
        {
            return _dao.AsNoTracking().ToListAsync();
        }
    }
}
