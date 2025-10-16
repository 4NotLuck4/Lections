using Lections1007.Model;
using Microsoft.EntityFrameworkCore;

namespace Lections1007.Contexts
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Game> Games => Set<Game>();

        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }
    }
}
