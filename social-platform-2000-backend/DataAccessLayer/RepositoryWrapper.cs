using social_platform_2000_backend.Interfaces;

namespace social_platform_2000_backend.DataAccessLayer;

public class RespositoryWrapper : IRepositoryWrapper
{
    private ApplicationDbContext _context;
    private ICategoryRepository _category;
    private IPostRepository _post;

    public ICategoryRepository Category
    {
        get
        {
            if (_category == null)
            {
                _category = new CategoryRepository(_context);
            }

            return _category;
        }
    }

    public IPostRepository Post
    {
        get
        {
            if (_post == null)
            {
                _post = new PostRepository(_context);
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
