using UserService.Domain;
using UserService.Infrastructure.Context;

namespace UserService.Infrastructure;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}
