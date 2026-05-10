using System.Security.AccessControl;
using Common.Domain.ValueObjects;
using Common.Query;
using CoreModule.Domain.Courses.Enums;

namespace CoreModule.Query.Courses.DTOs;

public class CourseDto : BaseDto
{
    public Guid TeacherId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid SubCategoryId { get; set; }
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageName { get; set; } = null!;
    public string? VideoName { get; set; }
    public CourseLevel CourseLevel { get; set; }
    public CourseStatus CourseStatus { get; set; }
    public int Price { get; set; }
    public DateTimeOffset LastUpdate { get; set; }
    public SeoData? SeoData { get; set; }
    public List<CourseSectionDto> Sections { get; set; } = [];
}

public class CourseSectionDto : BaseDto
{
    public Guid CourseId { get; set; }
    public string Title { get; set; } = null!;
    public int DisplayOrder { get; set; }
    public List<CourseEpisodeDto> Episodes { get; set; } = [];
}

public class CourseEpisodeDto : BaseDto
{
    public Guid SectionId { get; set; }
    public string Title { get; set; } = null!;
    public string EnglishTitle { get; set; } = null!;
    public Guid Token { get; set; }
    public TimeSpan Time { get; set; }
    public string VideoName { get; set; } = null!;
    public string? AttachmentName { get; set; }
    public bool IsActive { get; set; }
}

public class CourseFilterData : BaseDto
{
    public string ImageName { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public int Price { get; set; }
    public CourseActionStatus ActionStatus { get; set; }
    public int EpisodeCount { get; set; }

}

public class CourseFilterParams : BaseFilterParam
{
    public Guid? TeacherId { get; set; }
}

public class CourseFilterResult : BaseFilter<CourseFilterData>
{

}