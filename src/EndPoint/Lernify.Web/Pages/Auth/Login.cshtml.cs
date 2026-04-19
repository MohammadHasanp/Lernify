using Common.Application.SecurityUtil;
using Lernify.Web.Infrastructure.JwtUtil;
using Lernify.Web.Infrastructure.RazorUtil;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using UserModule.Core.Commands.Users.Register;
using UserModule.Core.Services;

namespace Lernify.Web.Pages.Auth
{
    [BindProperties]
    public class LoginModel(IUserService service, IConfiguration configuration) : BaseRazorPage
    {
        private readonly IUserService _userService = service;
        private readonly IConfiguration _configuration = configuration;

        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [Length(11, 11, ErrorMessage = "{0} باید 11 کارکتر باشد ")]
        public string Mobile { get; set; } = null!;

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MinLength(6, ErrorMessage = "{0} باید 6 کاراکتر باشد")]
        public string Password { get; set; } = null!;

        public bool IsRememberMe { get; set; }
        public IActionResult OnGet()
        {
            return User.Identity!.IsAuthenticated ? Redirect("/") : Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userService.GetUserByMobile(Mobile);
            if (user == null)
            {
                ErrorAlert("کاربری با مشخصات وارد شده یافت نشد");
                return Page();
            }


            var isComparePassword = Sha256Hasher.IsCompare(user.Password, Password);
            if (!isComparePassword)
            {
                ErrorAlert("کاربری با مشخصات وارد شده یافت نشد");
                return Page();
            }

            var token = JwtTokenBuilder.BuildToken(user, _configuration);
            if (IsRememberMe)
            {
                HttpContext.Response.Cookies.Append("Token", token, new CookieOptions()
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTimeOffset.UtcNow.AddDays(30),
                    SameSite = SameSiteMode.None,
                });
            }

            else
            {
                HttpContext.Response.Cookies.Append("Token", token, new CookieOptions()
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                });
            }
            SuccessAlert("عملیات با موفقیت انجام شد");
            return RedirectToPage("../Index");
        }
    }
}
