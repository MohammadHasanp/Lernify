using Common.Application;
using Common.Application.FileUtil.Enums;
using Common.Application.FileUtil.StorageFactory;
using Common.Application.FileUtil.StorageInterfaces;
using Common.Application.FileUtil.Validations;
using CoreModule.Application._Utilities;
using CoreModule.Domain.Courses.Models;
using CoreModule.Domain.Courses.Repository;
using CoreModule.Domain.Courses.Service;

namespace CoreModule.Application.Courses.Create;

public class CreateCourseHandler(ICourseRepository repository, ICourseService service, IStorageServiceFactory serviceFactory) : IBaseCommandHandler<CreateCourseCommand>
{
    private readonly ICourseRepository _courseRepository = repository;
    private readonly ICourseService _courseService = service;
    private readonly IStorageServiceFactory _factory = serviceFactory;
    public async Task<OperationResult> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var storageService = _factory.GetStorageService(EnStorageType.File);
        var imageName = await storageService.SaveFileAndGenerateName(request.ImageFile, CoreModuleDirectories.CourseImages);

        string? videoName = null;
        Guid courseId = Guid.NewGuid();

        if (request.VideoFile != null)
            if (request.VideoFile.IsValidVideoFile())
            {
                videoName = await storageService.SaveFileAndGenerateName(request.VideoFile, CoreModuleDirectories.CourseVideos(courseId));
            }
            else
                return OperationResult.Error("فایل وارد شده نامعتبر است");

        var course = new Course(request.TeacherId, request.Title, request.Description, imageName, videoName, _courseService, request.CourseLevel,
            request.Price, request.SeoData, request.SubCategoryId, request.CategoryId, request.Slug)
        {
            Id = courseId,
        };

        _courseRepository.Add(course);
        await _courseRepository.Save();
        return OperationResult.Success();
    }
}