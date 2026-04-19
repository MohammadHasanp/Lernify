using Common.Application;
using Microsoft.EntityFrameworkCore;
using UserModule.Data.Context;

namespace UserModule.Core.Commands.Users.Edit;

public class EditUserHandler : IBaseCommandHandler<EditUserCommand>
{
    private readonly UserContext _userContext;

    public EditUserHandler(UserContext userContext)
    {
        _userContext = userContext;
    }

    public async Task<OperationResult> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
        if (user == null)
            return OperationResult.Error();

        user.Name = request.Name;
        user.Family = request.Family;

        if (!string.IsNullOrWhiteSpace(request.Email))
            user.Email = request.Email;
        await _userContext.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}
