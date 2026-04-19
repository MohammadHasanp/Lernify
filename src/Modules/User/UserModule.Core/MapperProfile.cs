using AutoMapper;
using UserModule.Core.Commands.Users.UserNotifications.Create;
using UserModule.Core.Queries.Users.DTOs;
using UserModule.Data.Entities.UserNotifications;
using UserModule.Data.Entities.Users;

namespace UserModule.Core;

class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<CreateUserNotificationCommand, UserNotification>().ReverseMap();
    }
}
