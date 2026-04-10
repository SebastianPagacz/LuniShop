using UserService.Domain.ValueObject;

namespace UserService.Domain.Models;

public class UserModel
{
    public int Id { get; private set; }
    public Email Email { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public IReadOnlyCollection<RoleModel> Roles { get; } // N:N need to rewrite it
    private List<RoleModel> _roles;

    private UserModel() { }
    private UserModel(string email, string name, string passwordHash) 
    {

    }
    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception(); // ToDo: Domain exception

        Name = name;
    }

    public void SetPassword(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new Exception();

        PasswordHash = passwordHash;
    }

    public void SetEmail(string email)
    {
        Email = Email.CreateEmail(email); // need to think about data flow here, I think if email is bad Email VO will throw an exception
    }
}
