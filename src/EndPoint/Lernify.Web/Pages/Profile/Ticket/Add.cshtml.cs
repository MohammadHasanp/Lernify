using Lernify.Web.Infrastructure.RazorUtil;
using Lernify.Web.Infrastructure.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TicketModule.Services;
using TicketModule.Services.DTOs.Command;
using UserModule.Core.Services;

namespace Lernify.Web.Pages.Profile.Ticket;

[BindProperties]
public class AddModel(IUserService userService, ITicketService ticketService) : BaseRazorPage
{
    private readonly IUserService _userService = userService;
    private readonly ITicketService _ticketService = ticketService;

    [Display(Name = "عنوان تیکت")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; } = null!;

    [Display(Name = "متن تیکت")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Text { get; set; } = null!;

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        var user = await _userService.GetUserByMobile(User.GetUserMobile());
        var ticket = new CreateTicketCommand()
        {
            Mobile = user!.Mobile,
            OwnerFullName = $"{user.Name} {user.Family}",
            Text = Text,
            Title = Title,
            UserId = User.GetUserId(),
        };
        var result = await _ticketService.CreateTicket(ticket);
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}
