using Common.Application;
using MediatR;
using UserModule.Core.Commands.Users.ChangePassword;
using UserModule.Core.Commands.Users.Edit;
using UserModule.Core.Commands.Users.Register;
using UserModule.Core.Queries.Users.DTOs;
using UserModule.Core.Queries.Users.GetById;
using UserModule.Core.Queries.Users.GetByMobile;

namespace UserModule.Core.Services;

public class UserService(IMediator mediator) : IUserService
{
    private readonly IMediator _mediator = mediator;

    public async Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<UserDto?> GetUserById(Guid userId)
    {
        return await _mediator.Send(new GetUserByIdQuery(userId));
    }

    public async Task<UserDto?> GetUserByMobile(string mobile)
    {
        return await _mediator.Send(new GetUserByMobileQuery(mobile));
    }

    public async Task<OperationResult<Guid>> Register(RegisterUserCommand command) => await _mediator.Send(command);
}