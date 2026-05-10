namespace CoreModule.Application._Utilities;

public class CoreModuleDirectories
{
    public static string CvFileNames = "wwwroot/core/teacher";
    public static string CourseImages = "wwwroot/core/course/Images";
    public static string CourseVideos(Guid courseId) => $"wwwroot/core/course/Demo/{courseId}";

    public static string GetCourseImage(string imageName) => $"{CourseImages.Replace("wwwRoot", "")}/{imageName}";
}
