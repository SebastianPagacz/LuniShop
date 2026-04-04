using LuniShop.Domain;
using LuniShop.Infrastructure.Context;

namespace LuniShop.Infrastructure;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}
