using CoreModule.Application.Courses.Episodes.Add;
using CoreModule.Facade.Courses;
using CoreModule.Facade.Teachers;
using Lernify.Web.Infrastructure;
using Lernify.Web.Infrastructure.CustomValidation.IFormFile;
using Lernify.Web.Infrastructure.RazorUtil;
using Lernify.Web.Infrastructure.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Lernify.Web.Pages.Profile.Teacher.Courses.Sections.Episodes;

[ServiceFilter(typeof(TeacherActionFilter))]
[BindProperties]
public class AddModel(ICourseFacade courseFacade, ITeacherFaced teacherFaced) : BaseRazorPage
{
    private readonly ICourseFacade _courseFacade = courseFacade;
    private readonly ITeacherFaced _teacherFaced = teacherFaced;


    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; } = null!;

    [Display(Name = "عنوان انگلیسی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string EnglishTitle { get; set; } = null!;

    [Display(Name = "زمان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [RegularExpression(@"^([0-9]{1}|(?:0[0-9]|1[0-9]|2[0-3])+):([0-5]?[0-9])(?::([0-5]?[0-9])(?:.(\d{1,9}))?)?$", ErrorMessage = 
        "لطفا زمان را با فرمت درست وارد کنید")]
    public TimeSpan Time { get; set; }

    [Display(Name = "فایل ضمیمه")]
    public IFormFile? AttachmentFile { get; set; }

    [Display(Name = "ویدیو")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [FileType("mp4", ErrorMessage = "ویدیو نامعتبر است")]
    public IFormFile VideoFile { get; set; } = null!;

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

    public async Task<IActionResult> OnPost(Guid courseId, Guid sectionId)
    {
        var result = await _courseFacade.AddEpisode(new AddEpisodeCommand(courseId, sectionId, Title, EnglishTitle,
            Time, AttachmentFile, VideoFile, false));

        return RedirectAndShowAlert(result, RedirectToPage("../Index", new { courseId }));
    }
}

