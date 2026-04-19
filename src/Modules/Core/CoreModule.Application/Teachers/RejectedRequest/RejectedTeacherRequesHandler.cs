using Common.Application;
using CoreModule.Domain.Teachers.Repository;

namespace CoreModule.Application.Teachers.RejectedRequest;

public class RejectedTeacherRequesHandler(ITeacherRepository repository) : IBaseCommandHandler<RejectedTeacherRequestCommand>
{
    private readonly ITeacherRepository _repository = repository;

    public async Task<OperationResult> Handle(RejectedTeacherRequestCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _repository.GetTracking(request.TeacherId);
        if (teacher == null)
            return OperationResult.NotFound();

        _repository.Delete(teacher);
        await _repository.Save();
        return OperationResult.Success();
    }
}