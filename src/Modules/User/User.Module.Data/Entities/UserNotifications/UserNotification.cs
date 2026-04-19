
using Common.Domain;
using System.ComponentModel.DataAnnotations;

namespace UserModule.Data.Entities.UserNotifications;

public class UserNotification : Entity
{
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(2000)]
    public string Text { get; set; } = null!;
    public bool IsSeen { get; set; }

}
