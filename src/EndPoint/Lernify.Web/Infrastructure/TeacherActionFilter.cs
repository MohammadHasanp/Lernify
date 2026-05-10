using CoreModule.Domain.Teachers.Enums;
using CoreModule.Facade.Teachers;
using Lernify.Web.Infrastructure.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lernify.Web.Infrastructure;

public class TeacherActionFilter(ITeacherFaced teacherFaced) : ActionFilterAttribute
{
    private readonly ITeacherFaced _teacherFaced = teacherFaced;

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.HttpContext.User.Identity is { IsAuthenticated: false })
            context.Result = new RedirectResult("/");

        var teacher = await _teacherFaced.GetByUserId(context.HttpContext.User.GetUserId());
        if (teacher != null || teacher!.TeacherStatus != TeacherStatus.Active)
            context.Result = new RedirectResult("/Profile");


        await next();
    }
}