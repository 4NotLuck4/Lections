using Lection1007.Contexts;
using Lection1007.Models;
using Microsoft.EntityFrameworkCore;

namespace Lection1007.Services
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Game> Games => Set<Game>();

        public AppDbContext()
        {
            //Database.EnsureDeleted();   // удаление БД
            //Database.EnsureCreated();   // создание БД
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source = text.db");
            optionsBuilder.UseSqlServer("Data Source=mssql;Initial Catalog=ispp3102;Persist Security Info=True;User ID=ispp3102;Password=3102;Encrypt=True;Trust Server Certificate=True");
        }
    }
}
