using Lections1007.Contexts;
using Lections1007.FIlteres;
using Lections1007.Model;
using Microsoft.EntityFrameworkCore;

namespace Lections1007.Services
{
    public class GameService(StoreDbContext context)
    {
        private readonly StoreDbContext _context = context;

        public async Task<List<Game>> GetGamesAsync()
            => await _context.Games.ToListAsync();

        public async Task<List<Game>> GetGamesAsync(GameFilter? filter)
        {
            if (filter is null)
                return await _context.Games.ToListAsync();

            var games = context.Games.AsQueryable();

            if (filter.Price is not null)
                games = games.Where(g => g.Price < filter.Price);
            if (filter.Name is not null)
                games = games.Where(g => g.Name.Contains(filter.Name));
            if (true)
                games = games.Where(g => g.Category.Name.Contains(filter.Category));

            return await games.ToListAsync();
        }
    }
}
