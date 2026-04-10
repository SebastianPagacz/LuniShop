using UserService.Domain.Exceptions;

namespace UserService.Domain.Models;

public class RoleModel
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public bool IsDeleted { get; private set; } = false;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

    private RoleModel() { }
    private RoleModel(string name)
    {
        Name = name;
    }

    public static RoleModel Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Role name can't be empty.");

        return new RoleModel(name);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Role name can't be empty.");

        Name = name;
        Update();
    }
    
    public void Delete()
    {
        IsDeleted = true;
        Update();
    }

    public void Update()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
