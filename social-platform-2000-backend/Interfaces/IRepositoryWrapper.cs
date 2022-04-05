using social_platform_2000_backend.DataAccessLayer;

namespace social_platform_2000_backend.Interfaces;

// @Note(Avic): Here we wrap all the existing database repositories
// So that we can Depedency Inject the IRepositoryWrapper into all services.
// Usage: _wrapper.Category.method_from_IRepositoryBase
public interface IRepositoryWrapper
{
    ICategoriesRepository Category { get; }
    IPostsRepository Post { get; }
    Task SaveAsync();
}