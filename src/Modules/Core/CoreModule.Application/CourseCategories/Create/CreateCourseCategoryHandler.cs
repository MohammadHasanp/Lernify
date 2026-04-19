using Common.Application;
using CoreModule.Domain.Categories.DomainServices;
using CoreModule.Domain.Categories.Models;
using CoreModule.Domain.Categories.Repository;

namespace CoreModule.Application.CourseCategories.Create;

public class CreateCourseCategoryHandler(ICourseCategoryRepository repository, ICourseCategoryService service) : IBaseCommandHandler<CreateCourseCategoryCommand>
{
    private readonly ICourseCategoryRepository _repository = repository;
    private readonly ICourseCategoryService _service = service;
    public async Task<OperationResult> Handle(CreateCourseCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new CourseCategory(request.Title, request.Slug, null, _service);
        _repository.Add(category);
        await _repository.Save();
        return OperationResult.Success();
    }
}