using Infrastructure;
using sp2000.Application.Interfaces;

namespace sp2000.Interfaces;

// @Note(Avic): Here we wrap all the existing database repositories
// So that we can Depedency Inject the IRepositoryWrapper into all services.
// Usage: _wrapper.Category.method_from_IRepositoryBase
public interface IRepositoryWrapper
{
    ICategoriesRepository Category { get; }
    IPostsRepository Post { get; }
    ICommentsRepository Comment { get; }
    Task SaveAsync();
}