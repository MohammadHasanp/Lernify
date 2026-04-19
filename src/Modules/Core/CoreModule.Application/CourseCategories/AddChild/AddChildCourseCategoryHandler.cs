using Common.Application;
using CoreModule.Domain.Categories.DomainServices;
using CoreModule.Domain.Categories.Models;
using CoreModule.Domain.Categories.Repository;

namespace CoreModule.Application.CourseCategories.AddChild;

public class AddChildCourseCategoryHandler(ICourseCategoryRepository repository, ICourseCategoryService service) : IBaseCommandHandler<AddChildCourseCategoryCommand>
{
    private readonly ICourseCategoryRepository _repository = repository;
    private readonly ICourseCategoryService _service = service;
    public async Task<OperationResult> Handle(AddChildCourseCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new CourseCategory(request.Title, request.Slug, request.ParentId, _service);
        _repository.Add(category);
        await _repository.Save();
        return OperationResult.Success();
    }
}