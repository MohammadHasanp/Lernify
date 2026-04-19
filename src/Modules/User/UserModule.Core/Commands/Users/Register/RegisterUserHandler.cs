using Common.Application;
using Common.Application.SecurityUtil;
using Microsoft.EntityFrameworkCore;
using UserModule.Data.Entities.Users;
using UserModule.Data.Context;


namespace UserModule.Core.Commands.Users.Register;

public class RegisterUserHandler(UserContext context) : IBaseCommandHandler<RegisterUserCommand, Guid>
{
    private readonly UserContext _context = context;
    public async Task<OperationResult<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _context.Users.AnyAsync(u => u.Mobile == request.Mobile))
            return OperationResult<Guid>.Error("شماره تلفن تکراری است");

        var user = new User
        {
            Mobile = request.Mobile,
            Password = Sha256Hasher.Hash(request.Password),
            Avatar = "avatar.png",
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return OperationResult<Guid>.Success(user.Id);
    }
}