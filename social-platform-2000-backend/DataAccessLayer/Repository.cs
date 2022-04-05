using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using social_platform_2000_backend.Interfaces;

namespace social_platform_2000_backend.DataAccessLayer;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected ApplicationDbContext RepositoryContext { get; set; }
    public RepositoryBase(ApplicationDbContext repositoryContext)
    {
        RepositoryContext = repositoryContext;
    }
    public IQueryable<T> FindAll() => RepositoryContext.Set<T>().AsNoTracking();
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
        RepositoryContext.Set<T>().Where(expression).AsNoTracking();
    public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);
    public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
    public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
}