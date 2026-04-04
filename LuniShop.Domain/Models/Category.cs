using LuniShop.Domain.Exceptions;

namespace LuniShop.Domain.Models;

public class Category
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = true;
    public bool IsDeleted { get; private set; } = false;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

    private Category() { }
    private Category(string name)
    {
        Name = name;
    }
    public static Category Create(string name)
    {
        if(string.IsNullOrEmpty(name))
            throw new DomainException("Name can't be null or empty.");

        return new Category(name);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new DomainException("Name can't be null or empty.");

        Name = name;
        Update();
    }
    public void Delete()
    {
        IsDeleted = true;
        Update();
    }
    public void Deactivate()
    {
        IsActive = false;
        Update();
    }
    public void Activate()
    {
        IsActive = true;
        Update();
    }
    private void Update()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
