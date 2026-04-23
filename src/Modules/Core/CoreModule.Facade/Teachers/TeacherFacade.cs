using Common.Application;
using CoreModule.Application.Teachers.AcceptedRequest;
using CoreModule.Application.Teachers.Register;
using CoreModule.Application.Teachers.RejectedRequest;
using CoreModule.Query.Teachers.DTOs;
using CoreModule.Query.Teachers.GetById;
using CoreModule.Query.Teachers.GetByUserId;
using CoreModule.Query.Teachers.GetList;
using MediatR;

namespace CoreModule.Facade.Teachers;

public class TeacherFacade(IMediator mediator) : ITeacherFacede
{
    private readonly IMediator _mediator = mediator;
    public async Task<OperationResult> Accepted(AcceptedTeacherRequestCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<TeacherDto?> GetByUserId(Guid userId)
    {
        return await _mediator.Send(new GetTeacherByUserIdQuery(userId));
    }

    public async Task<List<TeacherDto>> GetList()
    {
        return await _mediator.Send(new GetListTeacherQuery());
    }

    public async Task<OperationResult> Register(RegisterTeacherCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Rejected(RejectedTeacherRequestCommand command)
    {
        return await _mediator.Send(command);
    }
}