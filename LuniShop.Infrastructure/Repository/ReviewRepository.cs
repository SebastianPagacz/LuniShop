using LuniShop.Domain.Models;
using LuniShop.Domain.Repository;
using LuniShop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LuniShop.Infrastructure.Repository;

public class ReviewRepository(AppDbContext context) : IRepository<Review>
{
    public void Add(Review item)
    {
        context.Reviews.Add(item);
    }

    public async Task<List<Review>> GetAsync()
    {
        return await context.Reviews.ToListAsync();
    }

    public async Task<Review> GetByIdAsync(int id)
    {
        return await context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
    }
}
