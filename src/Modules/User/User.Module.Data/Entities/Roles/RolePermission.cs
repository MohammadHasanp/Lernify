using Common.Domain;
using UserModule.Data.Entities._Enum;

namespace UserModule.Data.Entities.Roles;

public class RolePermission : Entity
{
    public Guid RoleId { get; set; }
    public Permission Permission { get; set; }

    public Role Role { get; set; } = new Role();
}
