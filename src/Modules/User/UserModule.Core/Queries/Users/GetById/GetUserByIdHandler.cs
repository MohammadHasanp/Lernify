using User.Module.Data.Context;
using UserModule.Core.Queries.Users.DTOs;

namespace UserModule.Core.Queries.Users.GetById;

using AutoMapper;
using Common.Query;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public class GetUserByIdHandler(UserContext context, IMapper mapper) : IQueryHandler<GetUserByIdQuery, UserDto?>
{
    private readonly UserContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
        if (user == null)
            return null!;
        return _mapper.Map<UserDto>(user);
    }
}