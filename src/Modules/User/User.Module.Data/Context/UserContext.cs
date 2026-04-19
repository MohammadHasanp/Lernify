using Microsoft.EntityFrameworkCore;
using UserModule.Data.Entities.Roles;
using UserModule.Data.Entities.UserNotifications;
using UserModule.Data.Entities.Users;


namespace UserModule.Data.Context;

public class UserContext(DbContextOptions<UserContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserNotification> Notifications { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
}
