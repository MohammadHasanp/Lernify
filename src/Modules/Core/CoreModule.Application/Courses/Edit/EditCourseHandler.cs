using Common.Application;
using Common.Application.FileUtil.StorageInterfaces;
using Common.Application.FileUtil.Validations;
using CoreModule.Application._Utilities;
using CoreModule.Domain.Courses.Repository;
using CoreModule.Domain.Courses.Service;

namespace CoreModule.Application.Courses.Edit;


public class EditCourseHandler(ICourseRepository repository, ICourseService service, ILocalFileService fileService) :
    IBaseCommandHandler<EditCourseCommand>
{
    private readonly ICourseRepository _courseRepository = repository;
    private readonly ICourseService _courseService = service;
    private readonly ILocalFileService _ftpFileService = fileService;
    public async Task<OperationResult> Handle(EditCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetTracking(request.CourseId);
        if (course == null)
            return OperationResult.NotFound();

        var oldVideoName = course.VideoName;
        var oldImageName = course.ImageName;

        var videoName = course.VideoName;
        var imageName = course.ImageName;

        if (request.VideoFile != null)
            if (request.VideoFile.IsValidVideoFile())
            {
                videoName = await _ftpFileService!.SaveFileAndGenerateName(request.VideoFile, CoreModuleDirectories.GetCourseVideos(course.Id));
            }
            else
                return OperationResult.Error("فایل وارد شده نامعتبر است");

        if (request.ImageFile.IsImage() && request.ImageFile != null)
        {
            imageName = await _ftpFileService!.SaveFileAndGenerateName(request.ImageFile, CoreModuleDirectories.CourseImages);
        }

        course.Edit(request.Title, request.Description, imageName, videoName, _courseService, request.CourseLevel, request.CourseStatus, request.Price,
            request.SeoData, request.SubCategoryId, request.CategoryId, request.Slug, request.ActionStatus);

        await _courseRepository.Save();

        DeleteOldFiles(oldImageName, oldVideoName, request);
        return OperationResult.Success();
    }

    private void DeleteOldFiles(string? oldImageName, string? oldVideoName, EditCourseCommand command)
    {
        if (oldVideoName != null && string.IsNullOrWhiteSpace(oldVideoName) == false)
            _ftpFileService.DeleteFile(CoreModuleDirectories.CourseVideos, oldVideoName);

        if (oldImageName != null && string.IsNullOrWhiteSpace(oldImageName) == false)
            _ftpFileService.DeleteFile(CoreModuleDirectories.CourseImages, oldImageName);
    }
}