using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.CourseCategories.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.CourseCategories.GetAll;

internal class GetAllCourseCategoryHandler(QueryContext queryContext) : IQueryHandler<GetAllCourseCategoryQuery, List<CourseCategoryDto>>
{
    private readonly QueryContext _queryContext = queryContext;

    public async Task<List<CourseCategoryDto>> Handle(GetAllCourseCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _queryContext.CourseCategories.Where(c => c.ParentId == null)
            .OrderBy(t => t.CreationDate)
            .Select(s => new CourseCategoryDto
            {
                Id = s.Id,
                CreationDate = s.CreationDate,
                IsDelete = s.IsDelete,
                Title = s.Title,
                Slug = s.Slug,
                ParentId = s.ParentId
            }).ToListAsync(cancellationToken);
    }
}