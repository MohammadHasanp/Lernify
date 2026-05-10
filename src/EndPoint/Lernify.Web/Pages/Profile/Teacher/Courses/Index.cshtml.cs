using CoreModule.Facade.Courses;
using CoreModule.Facade.Teachers;
using CoreModule.Query.Courses.DTOs;
using Lernify.Web.Infrastructure;
using Lernify.Web.Infrastructure.RazorUtil;
using Lernify.Web.Infrastructure.Util;
using Microsoft.AspNetCore.Mvc;

namespace Lernify.Web.Pages.Profile.Teacher.Courses;

[ServiceFilter(typeof(TeacherActionFilter))]
public class IndexModel(ICourseFacade courseFacade, ITeacherFaced teacherFaced) : BaseRazorFilter<CourseFilterParams>
{
    private readonly ICourseFacade _courseFacade = courseFacade;
    private readonly ITeacherFaced _teacherFaced = teacherFaced;

    public CourseFilterResult FilterResult { get; set; } = null!;

    public async Task OnGet()
    {
        var teacher = await _teacherFaced.GetByUserId(User.GetUserId());
        FilterParams.TeacherId = teacher!.Id;
        FilterResult = await _courseFacade.GetByFilter(FilterParams);
    }
}