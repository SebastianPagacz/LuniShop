using UserService.Domain.Exceptions;
using UserService.Domain.ValueObject;

namespace UserService.Domain.Models;

public class UserModel
{
    public int Id { get; private set; }
    public Email Email { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public bool IsDeleted { get; private set; } = false;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
    public IReadOnlyCollection<UserRole> UserRoles { get {return _userRoles; } }
    private List<UserRole> _userRoles = new();

    private UserModel() { }
    private UserModel(Email email, string name, string passwordHash) 
    {
        Email = email;
        Name = name;
        PasswordHash = passwordHash;

    }

    public static UserModel Create(string emailString, string name, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(emailString))
            throw new DomainException("Email can't be blank");

        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name can't be blank");

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new DomainException("Password can't be blank");

        var validEmail = Email.CreateEmail(emailString);
        
        return new UserModel(validEmail, name, passwordHash);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Username can't be empty.");

        Name = name;
        Update();
    }

    public void SetPassword(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new DomainException("Password can't be empty.");

        PasswordHash = passwordHash;
        Update();
    }

    public void SetEmail(string email)
    {
        Email = Email.CreateEmail(email);
        Update();
    }

    public void Delete()
    {
        IsDeleted = true;
    }

    private void Update()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddRole(int roleId) // one sided adding roles, User entity manages the roles
    {
        if(_userRoles.Any(ur => ur.RoleId == roleId))
            throw new DomainException("Role is already assigned to the user.");
        
        var newUserRole = UserRole.Create(this, roleId);
        _userRoles.Add(newUserRole);
        Update();
    }

    public void RemoveRole(int roleId)
    {
        _userRoles.RemoveAll(ur => ur.RoleId == roleId);
        Update();
    }
}
