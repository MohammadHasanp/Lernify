using CoreModule.Facade.Courses;
using CoreModule.Facade.Teachers;
using CoreModule.Query.Courses.DTOs;
using Lernify.Web.Infrastructure;
using Lernify.Web.Infrastructure.RazorUtil;
using Lernify.Web.Infrastructure.Util;
using Microsoft.AspNetCore.Mvc;

namespace Lernify.Web.Pages.Profile.Teacher.Courses.Sections;

[ServiceFilter(typeof(TeacherActionFilter))]
public class IndexModel(ICourseFacade courseFacade, ITeacherFaced teacherFaced) : BaseRazorPage
{
    private readonly ICourseFacade _courseFacade = courseFacade;
    private readonly ITeacherFaced _teacherFaced = teacherFaced;

    public CourseDto? CourseDto { get; set; }

    public async Task<IActionResult> OnGet(Guid courseId)
    {
        var teacher = await _teacherFaced.GetByUserId(User.GetUserId());
        var course = await _courseFacade.GetById(courseId);

        if (course == null || course.TeacherId != teacher?.Id)
        {
            return RedirectToPage("../Index");
        }

        CourseDto = course;
        return Page();
    }
}
