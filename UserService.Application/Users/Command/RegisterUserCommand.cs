using MediatR;

namespace UserService.Application.Users.Command;

public record RegisterUserCommand(string EmailString, string Name, string Password) : IRequest<Result<string>> { }