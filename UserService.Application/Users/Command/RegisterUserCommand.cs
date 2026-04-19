using MediatR;
using UserService.Domain.ValueObject;

namespace UserService.Application.Users.Command;

public record RegisterUserCommand(string EmailString, string Name, string Password) : IRequest<Result<string>> { }