using Common.Query;

namespace UserModule.Core.Queries.Users.DTOs;

public class UserDto : BaseDto
{
    public string? Name { get; set; }
    public string? Family { get; set; }
    public string Mobile { get; set; } = null!;
    public string? Email { get; set; }
    public string Avatar { get; set; } = null!;
    public string Password { get; set; } = null!;
    public List<RoleDto> Roles { get; set; } = [];
}

public class RoleDto
{
    public string Title { get; set; } = null!;
    public Guid Id { get; set; }
}