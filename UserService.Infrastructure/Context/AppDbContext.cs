using Microsoft.EntityFrameworkCore;
using UserService.Domain.Models;

namespace UserService.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region User
        modelBuilder.Entity<UserModel>()
            .HasKey(u => u.Id);
        
        modelBuilder.Entity<UserModel>()
            .Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(150);

        modelBuilder.Entity<UserModel>()
            .Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<UserModel>()
            .OwnsOne(u => u.Email);
        #endregion

        #region Role
        modelBuilder.Entity<RoleModel>()
            .HasKey(r => r.Id);

        modelBuilder.Entity<RoleModel>()
            .Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100);
        #endregion
        #region UserRole
        modelBuilder.Entity<UserRole>()
            .HasKey(ur => 
            new {
                    ur.UserId,
                    ur.RoleId
                });
        #endregion
    }

    public DbSet<UserModel> Users { get; set; }
    public DbSet<RoleModel> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
}
