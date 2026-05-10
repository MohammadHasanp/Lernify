using CoreModule.Facade.CourseCategories;
using CoreModule.Query.CourseCategories.DTOs;
using Lernify.Web.Infrastructure.RazorUtil;

namespace Lernify.Web.Areas.Admin.Pages.Courses.Categories;

public class IndexModel(ICourseCategoryFacade categoryFacade) : BaseRazorPage
{
    private readonly ICourseCategoryFacade _categoryFacade = categoryFacade;

    public List<CourseCategoryDto> CategoryDto { get; set; } = [];

    public async Task OnGet()
    {
        CategoryDto = await _categoryFacade.GetMainCategory();
    }
}

