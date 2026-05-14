using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Courses.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Courses.GetByFilter;

internal class GetCourseByFilterHandler(QueryContext queryContext) : IQueryHandler<GetCourseByFilterQuery, CourseFilterResult>
{
    private readonly QueryContext _queryContext = queryContext;
    public async Task<CourseFilterResult> Handle(GetCourseByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;
        var result = _queryContext.Courses
            .Include(c => c.Sections)
            .ThenInclude(c => c.Episodes)
            .OrderBy(c => c.LastUpdate).AsQueryable();

        if (@params.TeacherId != null)
            result = result.Where(c => c.TeacherId == @params.TeacherId);

        if (@params.ActionStatus != null)
            result = result.Where(d => d.ActionStatus == @params.ActionStatus);

        var skip = (@params.PageId - 1) * @params.Take;
        var courses = await result.Skip(skip).Take(@params.Take).ToListAsync(cancellationToken);
        var model = new CourseFilterResult
        {
            Datas = courses.Select(c => new CourseFilterData
            {
                Id = c.Id,
                CreationDate = c.CreationDate,
                IsDelete = c.IsDelete,
                ImageName = c.ImageName,
                Title = c.Title,
                Slug = c.Slug,
                ActionStatus = c.ActionStatus,
                Price = c.Price,
                EpisodeCount = c.Sections.Sum(s => s.Episodes.Count),
            }).ToList(),
        };

        model.GeneratePaging(result, @params.Take, @params.PageId);
        return model;
    }
}