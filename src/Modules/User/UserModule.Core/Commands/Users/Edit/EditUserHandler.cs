using Common.Application;
using Microsoft.EntityFrameworkCore;
using User.Module.Data.Context;

namespace UserModule.Core.Commands.Users.Edit;

public class EditUserHandler(UserContext userContext) : IBaseCommandHandler<EditUserCommand>
{
    public async Task<OperationResult> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken: cancellationToken);
        if (user == null)
            return OperationResult.Error();

        user.Name = request.Name;
        user.Family = request.Family;

        if (!string.IsNullOrWhiteSpace(request.Email))
            user.Email = request.Email;
        await userContext.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}
