using System.ComponentModel.DataAnnotations;
using CoreModule.Facade.Courses;
using Lernify.Web.Infrastructure.CustomValidation.IFormFile;
using Lernify.Web.Infrastructure.RazorUtil;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lernify.Web.Areas.Admin.Pages.Courses.Sections;

public class EditEpisodeModel(ICourseFacade courseFacade) : BaseRazorPage
{
    private readonly ICourseFacade _courseFacade = courseFacade;


    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; } = null!;

    [Display(Name = "زمان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [RegularExpression(@"^([0-9]{1}|(?:0[0-9]|1[0-9]|2[0-3])+):([0-5]?[0-9])(?::([0-5]?[0-9])(?:.(\d{1,9}))?)?$", ErrorMessage =
        "لطفا زمان را با فرمت درست وارد کنید")]
    public TimeSpan Time { get; set; }

    [Display(Name = "ویدیو")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [FileType("mp4", ErrorMessage = "ویدیو نامعتبر است")]
    public IFormFile? VideoFile { get; set; }

    [Display(Name = "فایل ضمیمه")]
    public IFormFile? AttachmentFile { get; set; }

    [Display(Name = "وضعیت")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public bool IsActive { get; set; }

    public async Task<IActionResult> OnGet(Guid episodeId)
    {
        var episode = await _courseFacade.GetEpisodeById(episodeId);
        if (episode == null)
            return RedirectToPage("../Index");

        Title = episode.Title;
        Time = episode.Time;
        IsActive = episode.IsActive;
        return Page();
    }
}
