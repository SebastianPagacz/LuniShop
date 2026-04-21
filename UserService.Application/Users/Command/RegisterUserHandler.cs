using MediatR;
using UserService.Application.Services;
using UserService.Domain;
using UserService.Domain.Models;
using UserService.Domain.Repository;
using UserService.Domain.ValueObject;

namespace UserService.Application.Users.Command;

public class RegisterUserHandler(IUserQueryService queryService, IRepository<UserModel> repository, IPasswordHasher passwordHasher, IUnitOfWork uow) : IRequestHandler<RegisterUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if(!Email.IsValidEmail(request.EmailString))
            return new Result<string>(false, Message: "Invalid email address.");

        var existingUser = await queryService.GetUserByDetailsAsync(request.EmailString, request.Name, cancellationToken); // Need to learn how VO work with ef core

        if (existingUser != null)
            return new Result<string>(false, Message: "Username or email is already in use.");

        string paswdHash = passwordHasher.Hash(request.Password);
        var newUser = UserModel.Create(request.EmailString, request.Name, paswdHash);
        repository.Add(newUser);

        await uow.SaveAsync(cancellationToken);
        
        return new Result<string>(true, Value: $"User with Id: {newUser.Id} has been created.");
    }
}
