using Common.Application;
using CoreModule.Application.CourseCategories.Create;
using CoreModule.Application.CourseCategories.Delete;
using CoreModule.Application.CourseCategories.Edit;
using CoreModule.Query.CourseCategories.DTOs;
using CoreModule.Query.CourseCategories.GetAll;
using CoreModule.Query.CourseCategories.GetChildren;
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

    public async Task<List<CourseCategoryDto>> GetMainCategory()
    {
        return await _mediator.Send(new GetAllCourseCategoryQuery());
    }

    public async Task<List<CourseCategoryDto>> GetChildrenCategory(Guid parentId)
    {
        return await _mediator.Send(new GetChildrenCategoryQuery(parentId));
    }

    public async Task<OperationResult> Edit(EditCourseCategoryCommand command)
    {
        return await _mediator.Send(command);
    }
}