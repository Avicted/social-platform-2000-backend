using sp2000.Interfaces;

namespace Infrastructure;

public class RespositoryWrapper : IRepositoryWrapper
{
    private ApplicationDbContext _context;
    private ICategoriesRepository _category;
    private IPostsRepository _post;

    public ICategoriesRepository Category
    {
        get
        {
            if (_category == null)
            {
                _category = new CategoriesRepository(_context);
            }

            return _category;
        }
    }

    public IPostsRepository Post
    {
        get
        {
            if (_post == null)
            {
                _post = new PostsRepository(_context);
            }

            return _post;
        }
    }

    public RespositoryWrapper(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
