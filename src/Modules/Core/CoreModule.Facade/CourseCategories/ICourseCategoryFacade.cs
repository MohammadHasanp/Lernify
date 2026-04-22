using Common.Application;
using CoreModule.Application.CourseCategories.Create;
using CoreModule.Application.CourseCategories.Delete;
using CoreModule.Application.CourseCategories.Edit;


namespace CoreModule.Facade.CourseCategories;

public interface ICourseCategoryFacade
{
    Task<OperationResult> Create(CreateCourseCategoryCommand command);
    Task<OperationResult> Edit(EditCourseCategoryCommand command);
    Task<OperationResult> Delete(DeleteCourseCategoryCommand command);
}
