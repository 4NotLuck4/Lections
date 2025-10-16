using Lections1007.Contexts;
using Lections1007.Model;
using Microsoft.EntityFrameworkCore;

namespace Lections1007.Services
{
    public class CategoryService(StoreDbContext context)
    {
        private readonly StoreDbContext _context = context;

        public async Task<List<Category>> GetCategoriesAsync()
            => await _context.Categories.ToListAsync();
    }
}
