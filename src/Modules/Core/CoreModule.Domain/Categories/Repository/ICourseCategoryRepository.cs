using Common.Domain.Repository;
using CoreModule.Domain.Categories.Models;

namespace CoreModule.Domain.Categories.Repository;

public interface ICourseCategoryRepository : IBaseRepository<CourseCategory>
{
    public Task Delete(CourseCategory category);
}
