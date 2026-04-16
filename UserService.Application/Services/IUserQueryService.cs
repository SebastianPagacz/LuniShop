using UserService.Application.Users.DTO;
using UserService.Domain.ValueObject;

namespace UserService.Application.Services;

public interface IUserQueryService
{
    Task<UserDto> GetUserByDetailsAsync(Email email, string name, CancellationToken cancellationToken);
}
