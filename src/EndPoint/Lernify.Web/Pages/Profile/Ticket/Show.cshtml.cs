using Common.Application;
using Lernify.Web.Infrastructure.RazorUtil;
using Lernify.Web.Infrastructure.Util;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TicketModule.Services;
using TicketModule.Services.DTOs.Command;
using TicketModule.Services.DTOs.Query;
using UserModule.Core.Services;

namespace Lernify.Web.Pages.Profile.Ticket;

public class ShowModel(ITicketService service, IUserService userService) : BaseRazorPage
{
    private readonly ITicketService _service = service;
    private readonly IUserService _userService = userService;

    public TicketDto Ticket { get; set; } = new TicketDto();

    [BindProperty]
    [Display(Name = "متن پیام")]
    [Required(ErrorMessage = "{0}را وارد کنید ")]
    public string Text { get; set; } = null!;

    public async Task<IActionResult> OnGet(Guid id)
    {
        var ticket = await _service.GetTicketById(id);

        if (ticket == null || ticket.UserId != User.GetUserId())
            return RedirectToPage("Index");

        Ticket = ticket;
        return Page();
    }

    public async Task<IActionResult> OnPost(Guid id)
    {
        var user = await _userService.GetUserByMobile(User.GetUserMobile());
        var message = new SendTicketCommand()
        {
            OwnerFullName = $"{user!.Name} {user.Family}",
            Text = Text,
            UserId = User.GetUserId(),
            TicketId = id,
        };

        var result = await _service.SendMessageInTicket(message);
        return RedirectAndShowAlert(result, RedirectToPage("Show", new { id }));
    }

    public async Task<IActionResult> OnPostCloseTicket(Guid id)
    {
        return await AjaxTryCatch(async () =>
        {
            var ticket = await _service.GetTicketById(id);
            if (ticket == null || ticket.UserId != User.GetUserId())
                return OperationResult.Error("تیکت یافت نشد");

            return await _service.CloseTicket(id);
        });
    }
}