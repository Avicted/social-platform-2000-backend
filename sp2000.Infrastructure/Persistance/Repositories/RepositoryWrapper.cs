using sp2000.Application.Interfaces;

namespace sp2000.Infrastructure.Persistance.Repositories;

public class RespositoryWrapper : IRepositoryWrapper
{
    private readonly ApplicationDbContext _context;
    private ICategoriesRepository _category;
    private IPostsRepository _post;
    private ICommentsRepository _comment;

    public RespositoryWrapper(ApplicationDbContext context)
    {
        _context = context;
        _category = new CategoriesRepository(_context);
        _post = new PostsRepository(_context);
        _comment = new CommentsRepository(_context);
    }

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

    public ICommentsRepository Comment
    {
        get
        {
            if (_comment == null)
            {
                _comment = new CommentsRepository(_context);
            }

            return _comment;
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
