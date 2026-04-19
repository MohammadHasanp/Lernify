using Common.Application;
using Common.Application.FileUtil.Enums;
using Common.Application.FileUtil.StorageFactory;
using Common.Application.FileUtil.Validations;
using CoreModule.Application._Utilities;
using CoreModule.Domain.Courses.Repository;
using CoreModule.Domain.Courses.Service;

namespace CoreModule.Application.Courses.Edit;

public class EditCourseHandler(ICourseRepository repository, ICourseService service, IStorageServiceFactory factory) : IBaseCommandHandler<EditCourseCommand>
{
    private readonly ICourseRepository _courseRepository = repository;
    private readonly ICourseService _courseService = service;
    private readonly IStorageServiceFactory _factory = factory;
    public async Task<OperationResult> Handle(EditCourseCommand request, CancellationToken cancellationToken)
    {
        var storageService = _factory.GetStorageService(EnStorageType.File);

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
                videoName = await storageService.SaveFileAndGenerateName(request.VideoFile, CoreModuleDirectories.CourseVideos(course.Id));
            }
            else
                return OperationResult.Error("فایل وارد شده نامعتبر است");

        if (request.ImageFile.IsImage() && request.ImageFile != null)
        {
            imageName = await storageService.SaveFileAndGenerateName(request.ImageFile, CoreModuleDirectories.CourseImages);
        }

        course.Edit(request.Title, request.Description, imageName, videoName, _courseService, request.CourseLevel, request.CourseStatus, request.Price,
            request.SeoData, request.SubCategoryId, request.CategoryId, request.Slug);

        await _courseRepository.Save();

        await DeleteOldFiles(oldImageName, oldVideoName, request);
        return OperationResult.Success();
    }

    async Task DeleteOldFiles(string oldImageName, string? oldVideoName, EditCourseCommand command)
    {
        var storageFtpService = _factory.GetStorageService(EnStorageType.File);
        var storageFileService = _factory.GetStorageService(EnStorageType.File);

        if (oldVideoName != null && string.IsNullOrWhiteSpace(oldVideoName) == false)
            storageFtpService.DeleteFile(CoreModuleDirectories.CourseVideos(command.CourseId), oldVideoName);

        if (oldImageName != null && string.IsNullOrWhiteSpace(oldImageName) == false)
            storageFileService.DeleteFile(CoreModuleDirectories.CourseImages, oldImageName);
    }
}