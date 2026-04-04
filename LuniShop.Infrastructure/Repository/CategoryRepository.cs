using LuniShop.Domain.Models;
using LuniShop.Domain.Repository;
using LuniShop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LuniShop.Infrastructure.Repository;

public class CategoryRepository(AppDbContext context) : IRepository<Category>
{
    public void Add(Category item)
    {
        context.Categories.Add(item);
    }

    public async Task<List<Category>> GetAsync(CancellationToken cancellationToken)
    {
        return await context.Categories.Where(c => !c.IsDeleted && c.IsActive).ToListAsync(cancellationToken);
    }

    public async Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.Categories.Where(c => !c.IsDeleted && c.IsActive).FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}
