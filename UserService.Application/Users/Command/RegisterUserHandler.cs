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
        var validEmail = Email.CreateEmail(request.EmailString);
        var existingUser = await queryService.GetUserByDetailsAsync(validEmail, request.Name, cancellationToken); // Need to learn how VO work with ef core

        if (existingUser != null)
            return new Result<string>(false, Message: "User already exisits.");

        string paswdHash = passwordHasher.Hash(request.Password);
        var newUser = UserModel.Create(validEmail.ToString(), request.Name, paswdHash);
        repository.Add(newUser);

        await uow.SaveAsync(cancellationToken);
        
        return new Result<string>(true, Value: $"User with Id: {newUser.Id} has been created.");
    }
}
