using CoreModule.Application.Courses.Edit;
using CoreModule.Domain.Courses.Enums;
using Lernify.Web.Infrastructure.RazorUtil;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CoreModule.Facade.Courses;
using Lernify.Web.Infrastructure.CustomValidation.IFormFile;
using Lernify.Web.Infrastructure.ViewModel;

namespace Lernify.Web.Areas.Admin.Pages.Courses;

public class EditModel(ICourseFacade courseFacade) : BaseRazorPage
{
    private readonly ICourseFacade _courseFacade = courseFacade;

    public Guid CategoryId { get; set; }
    public Guid SubCategoryId { get; set; }

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; } = null!;

    [Display(Name = "عنوان انگلیسی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Slug { get; set; } = null!;

    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Description { get; set; } = null!;

    [Display(Name = "عکس")]
    [FileImage(ErrorMessage = "عکس نامعتبر است")]
    public IFormFile? ImageFile { get; set; }

    [Display(Name = "ویدئو معرفی")]
    [FileType("mp4", ErrorMessage = "ویدئو معرفی نامعتبر است")]
    public IFormFile? VideoFile { get; set; }

    [Display(Name = "قیمت")]
    public int Price { get; set; }

    public SeoDataViewModel? SeoData { get; set; }

    [Display(Name = "سطح دوره")]
    public CourseLevel CourseLevel { get; set; }

    [Display(Name = "وضعیت دوره")]
    public CourseStatus CourseStatus { get; set; }

    [Display(Name = "وضعیت")]
    public CourseActionStatus ActionStatus { get; set; }

    public async Task<IActionResult> OnGet(Guid courseId)
    {
        var course = await _courseFacade.GetById(courseId);

        if (course == null)
            return RedirectToPage("Index");

        CategoryId = course.CategoryId;
        SubCategoryId = course.SubCategoryId;
        Title = course.Title;
        Slug = course.Slug;
        Description = course.Description;
        SeoData = SeoDataViewModel.ConvertToViewModel(course.SeoData);
        CourseLevel = course.CourseLevel;
        CourseStatus = course.CourseStatus;
        ActionStatus = course.ActionStatus;

        return Page();
    }

    public async Task<IActionResult> OnPost(Guid courseId)
    {
        var result = await _courseFacade.Edit(new EditCourseCommand(courseId, CategoryId, SubCategoryId, Title, Slug, Description, ImageFile, VideoFile, Price,
            SeoData.Map(), CourseLevel, CourseStatus, ActionStatus));

        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}