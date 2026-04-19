using Lernify.Web.Infrastructure.RazorUtil;
using Lernify.Web.Infrastructure.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TicketModule.Services;
using TicketModule.Services.DTOs.Query;
using UserModule.Core.Services;

namespace Lernify.Web.Pages.Profile.Ticket;

[BindProperties]
public class IndexModel(IUserService userService, ITicketService ticketService) : BaseRazorFilter<TicketFilterParams>
{
    private readonly IUserService _userService = userService;
    private readonly ITicketService _ticketService = ticketService;

    public required TicketFilterResult FilterResult { get; set; }
    public async Task OnGet()
    {
        FilterResult = await _ticketService.GetTicketByFilter(new TicketFilterParams()
        {
            PageId = FilterParams.PageId,
            Take = 5,
            UserId = User.GetUserId()
        });
    }
}
