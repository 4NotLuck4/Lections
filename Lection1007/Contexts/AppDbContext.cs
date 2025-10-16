using Lections1007.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lections1007.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Game> Games => Set<Game>();

        public AppDbContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source = testDb.sqlite");

            optionsBuilder
                .UseSqlServer(@"Data Source=mssql;Initial Catalog=ispp3102;User ID=ispp3102;Password=3102;Trust Server Certificate=True")
                .LogTo(Console.WriteLine, LogLevel.Information);
        }
    }
}
