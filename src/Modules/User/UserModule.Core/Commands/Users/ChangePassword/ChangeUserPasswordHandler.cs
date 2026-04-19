using Common.Application;
using Common.Application.SecurityUtil;
using Microsoft.EntityFrameworkCore;
using UserModule.Data.Context;

namespace UserModule.Core.Commands.Users.ChangePassword;

public class ChangeUserPasswordHandler : IBaseCommandHandler<ChangeUserPasswordCommand>
{
    private readonly UserContext _context;

    public ChangeUserPasswordHandler(UserContext context)
    {
        _context = context;
    }

    public async Task<OperationResult> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
        if (user == null)
            return OperationResult.NotFound();

        if (Sha256Hasher.IsCompare(user.Password, request.CurrentPasssword))
        {
            user.Password = Sha256Hasher.Hash(request.NewPassword);
            _context.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
            return OperationResult.Success();
        }

        else
            return OperationResult.Error("کلمه عبور نامعتبر است");
    }
}
