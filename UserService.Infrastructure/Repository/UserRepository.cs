using Microsoft.EntityFrameworkCore;
using UserService.Domain.Models;
using UserService.Domain.Repository;
using UserService.Infrastructure.Context;

namespace UserService.Infrastructure.Repository;

public class UserRepository(AppDbContext context) : IRepository<UserModel>
{
    public void Add(UserModel item)
    {
        context.Add(item);
    }

    public async Task<List<UserModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Users.ToListAsync(cancellationToken);
    }

    public async Task<UserModel> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.Users.Where(u => u.Id == id).FirstOrDefaultAsync(cancellationToken);
    }
}
