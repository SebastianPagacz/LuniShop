namespace UserService.Application.Services;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string passwordHashed);
}
