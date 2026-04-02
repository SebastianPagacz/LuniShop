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

    public async Task<List<Review>> GetAsync(CancellationToken cancellationToken)
    {
        return await context.Reviews.ToListAsync(cancellationToken);
    }

    public async Task<Review> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.Reviews.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }
}
