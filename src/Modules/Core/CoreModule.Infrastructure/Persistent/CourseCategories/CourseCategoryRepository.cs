using CoreModule.Domain.Categories;
using CoreModule.Domain.Categories.Repository;
using CoreModule.Infrastructure.Persistent._Context;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure._Utilities;

namespace CoreModule.Infrastructure.Persistent.CourseCategories;

public class CourseCategoryRepository : BaseRepository<CourseCategory, CoreModuleEfContext>, ICourseCategoryRepository
{
    public CourseCategoryRepository(CoreModuleEfContext context) : base(context)
    {
    }

    public async Task Delete(CourseCategory category)
    {
        var categoryHasCourse = await _context.Courses.AnyAsync(f => f.CategoryId == category.Id || f.SubCategoryId == category.Id);

        if (categoryHasCourse)
        {
            throw new Exception("این دسته بندی دارای چندین دوره است");
        }

        var children = await _context.CourseCategories.Where(r => r.ParentId == category.Id).ToListAsync();
        if (children.Any())
        {
            foreach (var child in children)
            {
                var isAnyCourse = await _context.Courses.AnyAsync(f => f.CategoryId == child.Id || f.SubCategoryId == child.Id);

                if (isAnyCourse)
                {
                    throw new Exception("این دسته بندی دارای چندین دوره است");
                }
                else
                {
                    _context.Remove(child);
                }
            }
        }
        _context.Remove(category);
    }
}
