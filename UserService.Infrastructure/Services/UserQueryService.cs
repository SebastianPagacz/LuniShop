using Microsoft.EntityFrameworkCore;
using UserService.Application.Services;
using UserService.Application.Users.DTO;
using UserService.Domain.ValueObject;
using UserService.Infrastructure.Context;

namespace UserService.Infrastructure.Services;

public class UserQueryService(AppDbContext context) : IUserQueryService
{
    public async Task<UserDto> GetUserByDetailsAsync(Email email, string username, CancellationToken cancellationToken)
    {
        return await context.Users
            .AsNoTracking()
            .Where(u => u.Email == email && u.Username == username)
            .Select(u => new UserDto(u.Id, u.Email, u.Username))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
