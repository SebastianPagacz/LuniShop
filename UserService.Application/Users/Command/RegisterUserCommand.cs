using MediatR;
using UserService.Domain.ValueObject;

namespace UserService.Application.Users.Command;

public record RegisterUserCommand(Email Email, string Name, string Password) : IRequest<Result<string>> { }