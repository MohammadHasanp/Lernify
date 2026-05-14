namespace CoreModule.Application._Utilities;

public class CoreModuleDirectories
{
    public static string CvFileNames = "wwwroot/core/teacher";
    public static string CourseImages = "wwwroot/core/course/Images";
    public static string ImageUpload = "wwwroot/core/Images/Upload";
    public static string CourseVideos = "wwwroot/core/course/videos";

    public static string CourseEpisode(Guid courseId, Guid token)
        => $"wwwroot/core/course/{courseId}/Episodes/{token}";

    public static string GetCourseEpisode(Guid courseId, Guid token, string fileName)
        => $"wwwroot/core/course/{courseId}/Episodes/{token}/{fileName}";

    public static string GetCourseVideos(Guid courseId) => $"{CourseVideos.Replace("wwwroot", "")}/{courseId}";
    public static string GetCourseImage(string imageName) => $"{CourseImages.Replace("wwwroot", "")}/{imageName}";
    public static string GetImageUpload(string imageName) => $"{ImageUpload.Replace("wwwroot", "")}/{imageName}";
}