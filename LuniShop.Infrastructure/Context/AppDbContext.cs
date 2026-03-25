using LuniShop.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LuniShop.Infrastructure.Context;

public class AppDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Product>()
            .Property(p => p.Id)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<Product>()
            .Property(p => p.Description)
            .HasMaxLength(1000);
    }
    public AppDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Product> Products { get; set; }
}
