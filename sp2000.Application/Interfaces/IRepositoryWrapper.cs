namespace sp2000.Application.Interfaces;

// @Note(Avic): Here we wrap all the existing database repositories
// So that we can Depedency Inject the IRepositoryWrapper into all services.
// Usage: _wrapper.Category.method_from_IRepositoryBase
public interface IRepositoryWrapper
{
    IApplicationUsersRepository ApplicationUser { get; }
    ICategoriesRepository Category { get; }
    IPostsRepository Post { get; }
    ICommentsRepository Comment { get; }
    Task SaveAsync();
}