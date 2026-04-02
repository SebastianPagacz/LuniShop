using LuniShop.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LuniShop.Infrastructure.Context;

public class AppDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region product
        modelBuilder.Entity<Product>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Product>()
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<Product>()
            .Property(p => p.Description)
            .HasMaxLength(1000);

        modelBuilder.Entity<Product>() // I guess that ef core would map it without explicit config, feels like a good practise though
            .HasMany(e => e.Reviews)
            .WithOne()
            .HasForeignKey(e => e.ProductId)
            .IsRequired();
        #endregion

        #region review
        modelBuilder.Entity<Review>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Review>()
            .Property(r => r.ProductId)
            .IsRequired();

        modelBuilder.Entity<Review>()
            .Property(r => r.Title)
            .HasMaxLength(150);

        modelBuilder.Entity<Review>()
            .Property(r => r.Content)
            .HasMaxLength(1000);
        #endregion
    }
    public AppDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Product> Products { get; set; }
    public DbSet<Review> Reviews { get; set; }
}
