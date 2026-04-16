using MediatR;
using UserService.Application.Services;
using UserService.Domain.Models;
using UserService.Domain.Repository;

namespace UserService.Application.Users.Command;

public class RegisterUserHandler(IUserQueryService queryService, IRepository<UserModel> repository) : IRequestHandler<RegisterUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await queryService.GetUserByDetailsAsync(request.Email, request.Name, cancellationToken);

        if (existingUser is null)
            return new Result<string>(false, Message: "User already exisits.");

        
    }
}
