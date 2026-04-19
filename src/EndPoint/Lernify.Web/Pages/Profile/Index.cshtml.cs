using Lernify.Web.Infrastructure.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using UserModule.Core.Queries.Users.DTOs;
using UserModule.Core.Services;

namespace Lernify.Web.Pages.Profile
{
    public class IndexModel(IUserService service, IUserNotificationService userNotification) : PageModel
    {
        private readonly IUserService _service = service;
        private readonly IUserNotificationService _userNotification = userNotification;

        public UserDto? UserDto { get; set; } = new UserDto();
        public List<UserNotificationFilterData> NewNotifications { get; set; } = [];

        public async Task OnGet()
        {
            UserDto = await _service.GetUserByMobile(User.GetUserMobile());
            var result = await _userNotification.GetByFilter(new UserNotificationFilterParams()
            {
                IsSeen = false,
                PageId = 1,
                Take = 5,
                UserId = UserDto!.Id,
            });
            NewNotifications = result.Datas;
        }
    }
}
