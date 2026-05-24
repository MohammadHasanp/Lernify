using Common.Domain;
using User.Module.Data.Entities.Roles;

namespace User.Module.Data.Entities.Users;

public class UserRole : Entity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public User User { get; set; } = new User();
    public Role Role { get; set; } = new Role();
}
