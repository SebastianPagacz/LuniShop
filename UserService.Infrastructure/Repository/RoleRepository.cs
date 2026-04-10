using Microsoft.EntityFrameworkCore;
using UserService.Domain.Models;
using UserService.Domain.Repository;
using UserService.Infrastructure.Context;

namespace UserService.Infrastructure.Repository;

public class RoleRepository(AppDbContext context) : IRepository<RoleModel>
{
    public void Add(RoleModel item)
    {
        context.Roles.Add(item);
    }

    public async Task<List<RoleModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Roles.ToListAsync(cancellationToken);
    }

    public async Task<RoleModel> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.Roles.Where(r => r.Id == id).FirstOrDefaultAsync(cancellationToken);
    }
}
