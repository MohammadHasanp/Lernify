namespace TicketModule.Services.DTOs.Command;

public record SendTicketCommand
{
    public Guid TicketId { get; set; }
    public Guid UserId { get; set; }
    public string OwnerFullName { get; set; } = null!;
    public string Text { get; set; } = null!;
}