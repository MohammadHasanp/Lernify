using BlogModule.Context;
using BlogModule.Domain;
using Common.Domain.Repository;
using Shop.Infrastructure._Utilities;

namespace BlogModule.Repositories.Posts;

interface IPostRepository : IBaseRepository<Post>
{
    public void Delete(Post post);
}

class PostRepository : BaseRepository<Post, BlogContext>, IPostRepository
{
    public PostRepository(BlogContext context) : base(context)
    {
    }

    public void Delete(Post post)
    {
        _context.Posts.Remove(post);
    }
}