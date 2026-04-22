using Common.Application;
using CoreModule.Application.Teachers.AcceptedRequest;
using CoreModule.Application.Teachers.Register;
using CoreModule.Application.Teachers.RejectedRequest;

namespace CoreModule.Facade.Teachers;

public interface ITeacherFacede
{
    public Task<OperationResult> Register(RegisterTeacherCommand command);
    public Task<OperationResult> Rejected(RejectedTeacherRequestCommand command);
    public Task<OperationResult> Accepted(AcceptedTeacherRequestCommand command);
}
