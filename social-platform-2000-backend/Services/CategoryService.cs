using AutoMapper;
using social_platform_2000_backend.Models;
using social_platform_2000_backend.DTO;
using social_platform_2000_backend.Interfaces;

namespace social_platform_2000_backend.Services;

public class CategoryService : ICategoryService
{
    private IRepositoryWrapper _repository;
    private readonly IMapper _mapper;

    public CategoryService(IRepositoryWrapper repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<CategoryDto> CreateCategory(CreateCategoryDto category)
    {
        var entity = _mapper.Map<Category>(category);

        _repository.Category.Create(entity);

        await _repository.SaveAsync();

        return _mapper.Map<CategoryDto>(entity);
    }

    public async Task<CustomApiResponse> GetCategories(int? pageNumber)
    {
        var categories = await _repository.Category.GetAllGategoriesAsync();

        if (categories == null)
        {
            return new CustomApiResponse(
                payload: new object(),
                message: "No categories found",
                statusCode: 204
            );
        }

        const int pageSize = 10;
        // var temp = await PaginatedList<Category>.CreateAsync((IQueryable<Category>)categories, pageNumber ?? 1, pageSize);
        var result = _mapper.Map<List<CategoryDto>>(categories);


        return new CustomApiResponse(
            payload: result,
            new Pagination
            {
                CurrentPage = pageNumber ?? 1,
                PageSize = pageSize,
                TotalItemsCount = result.Count(),
                TotalPages = (int)Math.Ceiling(result.Count() / (double)pageSize),
            }
        );
    }

    public async Task<CategoryDto?> GetCategoryByID(int id)
    {
        var category = await _repository.Category.GetCategoryByIdAsync(id);

        if (category == null)
        {
            return null;
        }

        var categoryDto = _mapper.Map<CategoryDto>(category);

        return categoryDto;
    }

    public async Task<CategoryDto?> UpdateCategory(int id, UpdateCategoryDto category)
    {
        // Retrieve entity by id
        var entity = await _repository.Category.GetCategoryByIdAsync(id);

        // Validate entity is not null
        if (entity != null)
        {
            entity.Title = category.Title;

            _repository.Category.UpdateCategory(entity);

            // Save changes to the database
            await _repository.SaveAsync();

            // @Note(Avic): EntityFramework core will have updated the entity at this point
            // So we can return "the same" entity with updated field(s)
            return _mapper.Map<CategoryDto>(entity);
        }

        return null;
    }

    public async Task<bool> DeleteCategory(int id)
    {
        // Retrieve entity by id
        var entity = await _repository.Category.GetCategoryByIdAsync(id);

        // Validate entity is not null
        if (entity != null)
        {
            _repository.Category.Delete(entity);

            // Save changes to the database
            await _repository.SaveAsync();
        }
        else
        {
            return false;
        }

        return true;
    }
}