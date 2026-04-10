using System.ComponentModel;

namespace UserService.Domain.Models;

public class UserRole
{
    public int UserId { get; private set; }
    public UserModel User { get; private set; } = null!;
    public int RoleId { get;private set; }
    public RoleModel Role { get; private set; } = null!;

    private UserRole() { }
    private UserRole(UserModel user, int roleId)
    {
        User = user;
        UserId = user.Id;
        RoleId = roleId;
    }
    internal static UserRole Create(UserModel user, int roleId)
    {
        return new UserRole(user, roleId);
    }
}