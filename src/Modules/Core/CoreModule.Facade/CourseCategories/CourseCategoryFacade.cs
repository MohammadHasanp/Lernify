using Common.Application;
using CoreModule.Application.CourseCategories.Create;
using CoreModule.Application.CourseCategories.Delete;
using CoreModule.Application.CourseCategories.Edit;
using MediatR;

namespace CoreModule.Facade.CourseCategories;

public class CourseCategoryFacade(IMediator mediator) : ICourseCategoryFacade
{
    private readonly IMediator _mediator = mediator;
    public async Task<OperationResult> Create(CreateCourseCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Delete(DeleteCourseCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditCourseCategoryCommand command)
    {
        return await _mediator.Send(command);
    }
}