using System.ComponentModel.DataAnnotations;
using CoreModule.Application.Courses.Sections.Add;
using CoreModule.Facade.Courses;
using CoreModule.Facade.Teachers;
using Lernify.Web.Infrastructure.RazorUtil;
using Lernify.Web.Infrastructure.Util;
using Microsoft.AspNetCore.Mvc;

namespace Lernify.Web.Pages.Profile.Teacher.Courses.Sections;

[BindProperties]
public class AddModel(ICourseFacade courseFacade, ITeacherFaced teacherFaced) : BaseRazorPage
{
    private readonly ICourseFacade _courseFacade = courseFacade;
    private readonly ITeacherFaced _teacherFaced = teacherFaced;


    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; } = null!;

    [Display(Name = "ترتیب نمایش")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public int DisplayOrder { get; set; }


    public async Task<IActionResult> OnGet(Guid courseId)
    {
        var teacher = await _teacherFaced.GetByUserId(User.GetUserId());
        var course = await _courseFacade.GetById(courseId);

        if (course == null || course.TeacherId != teacher?.Id)
        {
            return RedirectToPage("../Index");
        }

        return Page();
    }

    public async Task<IActionResult> OnPost(Guid courseId)
    {
        var result = await _courseFacade.AddSection(new AddSectionCommand(Title, DisplayOrder, courseId));
        return RedirectAndShowAlert(result, RedirectToPage("Index", new { courseId }));
    }
}

