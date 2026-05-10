using AutoMapper;
using BlogModule.Domain;
using BlogModule.Repositories.Categories;
using BlogModule.Repositories.Posts;
using BlogModule.Services.DTOs.Command;
using BlogModule.Services.DTOs.Query;
using BlogModule.Utilities;
using Common.Application;
using Common.Application.FileUtil.StorageInterfaces;
using Common.Application.FileUtil.Validations;
using Common.Application.SecurityUtil;

namespace BlogModule.Services;

public interface IBlogService
{
    public Task<OperationResult> CreateCategory(CreateCategoryCommand command);
    public Task<OperationResult> EditCategory(EditCategoryCommand command);
    public Task<OperationResult> DeleteCategory(Guid id);
    public Task<List<BlogCategoryDto>> GetAllCategories();
    public Task<BlogCategoryDto> GetCategoryById(Guid id);

    public Task<OperationResult> CreatePost(CreatePostCommand command);
    public Task<OperationResult> EditPost(EditPostCommand command);
    public Task<OperationResult> DeletePost(Guid id);
    public Task<BlogPostDto> GetPostById(Guid id);
}

class CategoryService : IBlogService
{
    private readonly ICategoryRepository _CategoryRepository;
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;
    private readonly ILocalFileService _localFileService;
    public CategoryService(ICategoryRepository repository, IMapper mapper, IPostRepository postRepository, ILocalFileService localFileService)
    {
        _CategoryRepository = repository;
        _mapper = mapper;
        _postRepository = postRepository;
        _localFileService = localFileService;
    }

    public async Task<OperationResult> CreateCategory(CreateCategoryCommand command)
    {
        var category = _mapper.Map<Category>(command);
        if (await _CategoryRepository.ExistAsync(c => c.Slug == category.Slug))
            return OperationResult.Error("slig Is Exist");

        _CategoryRepository.Add(category);
        await _CategoryRepository.Save();
        return OperationResult.Success();
    }

    public async Task<OperationResult> CreatePost(CreatePostCommand command)
    {
        var post = _mapper.Map<Post>(command);
        if (await _postRepository.ExistAsync(p => p.Slug == command.Slug))
            return OperationResult.Error("Slug Is Exist");

        if (!command.ImageFile.IsImage())
            return OperationResult.Error("Image Invalid");

        var imageName = await _localFileService.SaveFileAndGenerateName(command.ImageFile, BlogDirectories.PostImage);

        post.ImageName = imageName;
        post.Visit = 0;
        post.Description = command.Description.SanitizeText();

        _postRepository.Add(post);
        await _postRepository.Save();
        return OperationResult.Success();
    }

    public async Task<OperationResult> DeleteCategory(Guid id)
    {
        var category = await _CategoryRepository.GetAsync(id);
        if (category != null)
            return OperationResult.NotFound();

        if (await _postRepository.ExistAsync(p => p.CategoryId == id))
            return OperationResult.Error("این دسته بندی قبلااستفاده شده است ابتدا پست مربوط را پاک کرده بعد تلاش کنید");

        _CategoryRepository.Delete(category!);
        await _CategoryRepository.Save();
        return OperationResult.Success();
    }

    public async Task<OperationResult> DeletePost(Guid id)
    {
        var post = await _postRepository.GetAsync(id);
        if (post == null)
            return OperationResult.NotFound();

        _postRepository.Delete(post);
        await _postRepository.Save();
        _localFileService.DeleteFile(BlogDirectories.PostImage, post.ImageName);
        return OperationResult.Success();
    }

    public async Task<OperationResult> EditCategory(EditCategoryCommand command)
    {
        var category = await this._CategoryRepository.GetAsync(command.CategoryId);
        if (category == null)
            return OperationResult.NotFound();

        if (category.Slug != command.Slug)
        {
            if (await _CategoryRepository.ExistAsync(c => c.Slug == command.Slug))
                return OperationResult.Error("slig Is Exist");
        }

        _mapper.Map<Category>(command);
        _CategoryRepository.Update(category);
        await _CategoryRepository.Save();
        return OperationResult.Success();

    }

    public async Task<OperationResult> EditPost(EditPostCommand command)
    {
        var post = await _postRepository.GetAsync(command.Id);
        if (post == null)
            return OperationResult.NotFound();

        if (post.Slug != command.Slug)
            if (await _postRepository.ExistAsync(p => p.Slug == command.Slug))
                return OperationResult.Error("Slug Is Exist");

        if (command.ImageFile != null)
            if (!command.ImageFile.IsImage())
                return OperationResult.Error("image Invalid");

            else
            {
                var imageName = await _localFileService.SaveFileAndGenerateName(command.ImageFile, BlogDirectories.PostImage);
                post.ImageName = imageName;
            }

        _mapper.Map<Post>(command);
        post.Description = command.Description.SanitizeText();

        _postRepository.Update(post);
        await _postRepository.Save();
        return OperationResult.Success();

    }
    public async Task<List<BlogCategoryDto>> GetAllCategories()
    {
        var categories = _CategoryRepository.GetAll();
        return _mapper.Map<List<BlogCategoryDto>>(categories);
    }

    public async Task<BlogCategoryDto> GetCategoryById(Guid id)
    {
        var category = _CategoryRepository.GetAsync(id);
        return _mapper.Map<BlogCategoryDto>(category);
    }

    public async Task<BlogPostDto> GetPostById(Guid id)
    {
        var post = await _postRepository.GetAsync(id);
        return _mapper.Map<BlogPostDto>(post);
    }
}