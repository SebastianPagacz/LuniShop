using UserService.Application.Services;

namespace UserService.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verify(string password, string passwordHashed)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHashed);
    }
}
