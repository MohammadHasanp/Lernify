using Common.Application;
using CoreModule.Domain.Teachers.Repository;

namespace CoreModule.Application.Teachers.AcceptedRequest;

public class AcceptedTeacherRequestHandler(ITeacherRepository repository) : IBaseCommandHandler<AcceptedTeacherRequestCommand>
{
    private readonly ITeacherRepository _repository = repository;
    public async Task<OperationResult> Handle(AcceptedTeacherRequestCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _repository.GetTracking(request.TeacherId);

        if (teacher == null)
            return OperationResult.NotFound();

        teacher.AcceptRequest();
        await _repository.Save();
        return OperationResult.Success();
    }
}