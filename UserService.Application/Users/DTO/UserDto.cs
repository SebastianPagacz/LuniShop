using UserService.Domain.ValueObject;

namespace UserService.Application.Users.DTO;

public record UserDto(int Id, Email Email, string Username) { }