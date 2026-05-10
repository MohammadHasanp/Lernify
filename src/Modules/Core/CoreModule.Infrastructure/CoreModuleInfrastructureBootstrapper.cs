using CoreModule.Domain.Categories.Repository;
using CoreModule.Domain.Courses.Repository;
using CoreModule.Domain.Teachers.Repository;
using CoreModule.Infrastructure.Persistent._Context;
using CoreModule.Infrastructure.Persistent.CourseCategories;
using CoreModule.Infrastructure.Persistent.Courses;
using CoreModule.Infrastructure.Persistent.Teachers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreModule.Infrastructure;

public static class CoreModuleInfrastructureBootstrapper
{
    public static IServiceCollection RegisterDependency(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<CoreModuleEfContext>(option => option.UseSqlServer(config.GetConnectionString("Core-Context")));

        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICourseCategoryRepository, CourseCategoryRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();

        return services;
    }
}
