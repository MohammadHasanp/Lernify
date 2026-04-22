using Common.Application;
using CoreModule.Application.Courses.Create;
using CoreModule.Application.Courses.Edit;

namespace CoreModule.Facade.Courses;

public interface ICourseFacade
{
    public Task<OperationResult> Create(CreateCourseCommand command);
    public Task<OperationResult> Edit(EditCourseCommand command);
}
