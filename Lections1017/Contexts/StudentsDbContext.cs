using Lections1017.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lections1017.Contexts
{
    public partial class StudentsDbContext : DbContext
    {
        public virtual DbSet<Group> Groups { get; set; }
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = students.db");
            //optionsBuilder.UseSqlServer(@"Data Source=mssql;Initial Catalog=ispp3102;User ID=ispp3102;Password=3102;Trust Server Certificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().HasData(
                new Group { Id = 1, Title = "ИСПП-31" },
                new Group { Id = 2, Title = "ИСПП-34" },
                new Group { Id = 3, Title = "ИСПП-45" }
                );
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
