using Common.Domain;
using System.ComponentModel.DataAnnotations;

namespace UserModule.Data.Entities.Roles;

public class Role : Entity
{
    [MaxLength(50)]
    [Required]
    public string Name { get; set; } = null!;
    public List<RolePermission> Permissions { get; set; } = [];
}
