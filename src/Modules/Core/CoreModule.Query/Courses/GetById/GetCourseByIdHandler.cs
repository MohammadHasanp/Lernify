using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Courses.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Courses.GetById;

internal class GetCourseByIdHandler(QueryContext queryContext) : IQueryHandler<GetCourseByIdQuery, CourseDto?>
{
    private readonly QueryContext _queryContext = queryContext;
    public async Task<CourseDto?> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await _queryContext.Courses
            .Include(c => c.Sections)
            .ThenInclude(r => r.Episodes)
            .FirstOrDefaultAsync(u => u.Id == request.CourseId, cancellationToken);

        if (course == null)
            return null;

        return new CourseDto()
        {
            Id = course.Id,
            CreationDate = course.CreationDate,
            IsDelete = course.IsDelete,
            TeacherId = course.TeacherId,
            CategoryId = course.CategoryId,
            SubCategoryId = course.SubCategoryId,
            Title = course.Title,
            Slug = course.Slug,
            Description = course.Description,
            ImageName = course.ImageName,
            VideoName = course.VideoName,
            CourseLevel = course.CourseLevel,
            CourseStatus = course.CourseStatus,
            Price = course.Price,
            LastUpdate = course.LastUpdate,
            SeoData = course.SeoData,
            ActionStatus = course.ActionStatus,
            Sections = course.Sections.Select(f => new CourseSectionDto
            {
                Id = f.Id,
                CreationDate = f.CreationDate,
                IsDelete = f.IsDelete,
                CourseId = f.CourseId,
                Title = f.Title,
                DisplayOrder = f.DisplayOrder,
                Episodes = f.Episodes.Select(e => new CourseEpisodeDto
                {
                    Id = e.Id,
                    CreationDate = e.CreationDate,
                    IsDelete = e.IsDelete,
                    SectionId = e.SectionId,
                    Title = e.Title,
                    EnglishTitle = e.EnglishTitle,
                    Token = e.Token,
                    Time = e.Time,
                    VideoName = e.VideoName,
                    AttachmentName = e.AttachmentName,
                    IsActive = e.IsActive
                }).ToList()
            }).ToList()
        };
    }
}