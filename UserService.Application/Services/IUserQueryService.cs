using UserService.Application.Users.DTO;
using UserService.Domain.ValueObject;

namespace UserService.Application.Services;

public interface IUserQueryService
{
    Task<UserDto> GetUserByDetailsAsync(string emailString, string name, CancellationToken cancellationToken);
}
