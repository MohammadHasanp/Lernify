using CoreModule.Domain.Courses.Models;
using CoreModule.Domain.Courses.Repository;
using CoreModule.Infrastructure.Persistent._Context;
using Shop.Infrastructure._Utilities;

namespace CoreModule.Infrastructure.Persistent.Courses;

public class CourseRepository : BaseRepository<Course, CoreModuleEfContext>, ICourseRepository
{
    public CourseRepository(CoreModuleEfContext context) : base(context)
    {
    }
}
