namespace UserService.Domain.Models;

public class RoleModel
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    private RoleModel() { }
    private RoleModel(string name)
    {
        
    }

    public static RoleModel Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception();

        return new RoleModel(name);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception();

        Name = name;
    }
}
