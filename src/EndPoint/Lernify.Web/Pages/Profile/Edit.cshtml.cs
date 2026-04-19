using Lernify.Web.Infrastructure.RazorUtil;
using Lernify.Web.Infrastructure.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using UserModule.Core.Commands.Users.Edit;
using UserModule.Core.Services;

namespace Lernify.Web.Pages.Profile;

[BindProperties]
public class EditModel(IUserService userService) : BaseRazorPage
{
    private readonly IUserService _userService = userService;

    [Display(Name = "نام")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Name { get; set; } = null!;

    [Display(Name = "نام خانوادگی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Family { get; set; } = null!;

    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    public async Task OnGet()
    {
        var user = await _userService.GetUserById(User.GetUserId());
        if (user != null)
        {
            Name = user.Name;
            Family = user.Family;
            Email = user.Email;
        }
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _userService.Edit(new EditUserCommand(User.GetUserId(), Family, Name, Email));
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}
