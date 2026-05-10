using Common.Application;
using CoreModule.Application.Courses.Create;
using CoreModule.Application.Courses.Edit;
using CoreModule.Application.Courses.Episodes.Add;
using CoreModule.Application.Courses.Sections.Add;
using CoreModule.Query.Courses.DTOs;

namespace CoreModule.Facade.Courses;

public interface ICourseFacade
{
    public Task<OperationResult> Create(CreateCourseCommand command);
    public Task<OperationResult> Edit(EditCourseCommand command);
    public Task<OperationResult> AddSection(AddSectionCommand command);
    public Task<OperationResult> AddEpisode(AddEpisodeCommand command);


    public Task<CourseFilterResult> GetByFilter(CourseFilterParams filterParams);
    public Task<CourseDto?> GetById(Guid courseId);
}
