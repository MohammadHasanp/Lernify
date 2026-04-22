using Common.Application;
using CoreModule.Application.Courses.Create;
using CoreModule.Application.Courses.Edit;
using MediatR;

namespace CoreModule.Facade.Courses;

public class CourseFacade(IMediator mediator) : ICourseFacade
{
    private readonly IMediator _mediator = mediator;
    public async Task<OperationResult> Create(CreateCourseCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditCourseCommand command)
    {
        return await _mediator.Send(command);
    }
}
