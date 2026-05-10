using Common.Query;
using CoreModule.Domain.Courses.Enums;

namespace CoreModule.Query.Courses.DTOs;

public class CourseDto : BaseDto
{

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