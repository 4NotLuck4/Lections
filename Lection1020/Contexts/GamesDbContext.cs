using System;
using System.Collections.Generic;
using Lection1020.Models;
using Microsoft.EntityFrameworkCore;

namespace Lection1020.Contexts;

public partial class GamesDbContext : DbContext
{
    public GamesDbContext()
    {
    }

    public GamesDbContext(DbContextOptions<GamesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=mssql;Initial Catalog=ispp3102;User ID=ispp3102;Password=3102;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0BAE31812D");

            entity.ToTable("Category");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.GameId).HasName("PK__Game__2AB897FDDA40E25D");

            entity.ToTable("Game", tb => tb.HasTrigger("TrChangePrice"));

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.KeysAmount).HasDefaultValue((short)100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(16, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.Games)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Game_Category");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
