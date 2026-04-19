namespace CoreModule.Domain.Categories.DomainServices;

public interface ICourseCategoryService
{
    public bool IsExistsSlug(string slug);
}
