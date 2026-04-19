using BlogModule.Context;
using BlogModule.Domain;
using Common.Domain.Repository;
using Shop.Infrastructure._Utilities;

namespace BlogModule.Repositories.Categories
{
    interface ICategoryRepository : IBaseRepository<Category>
    {
        public void Delete(Category category);
        public List<Category> GetAll();
    }

    class CategoryRepository : BaseRepository<Category, BlogContext>, ICategoryRepository
    {
        public CategoryRepository(BlogContext context) : base(context)
        {
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }
    }
}
