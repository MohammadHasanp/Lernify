using Common.Query;
using TicketModule.Domain;

namespace TicketModule.Services.DTOs.Query;

public class TicketFilterData : BaseDto
{
    public Guid UserId { get; set; }
    public TicketStatus TicketStatus { get; set; }
    public string Title { get; set; } = null!;
}

public class TicketFilterParams : BaseFilterParam
{
    public Guid? UserId { get; set; }
}

public class TicketFilterResult : BaseFilter<TicketFilterData>
{

}