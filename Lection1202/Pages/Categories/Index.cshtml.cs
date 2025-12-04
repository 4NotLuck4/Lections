using Lection1202.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lection1202.Pages.Categories
{
    public class IndexModel : AccessPageModel
    {
        private readonly Lection1202.Contexts.GamesDbContext _context;

        public IndexModel(Lection1202.Contexts.GamesDbContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (HasRole() is IActionResult action)
                return action;

            Category = await _context.Categories.ToListAsync();
            return Page();
        }
    }
}
