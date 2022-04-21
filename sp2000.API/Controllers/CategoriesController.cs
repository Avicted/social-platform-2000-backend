using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using sp2000.Application.DTO;
using sp2000.Application.Models;
using sp2000.Application.Helpers;
using sp2000.Application.Interfaces;

namespace sp2000.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesService _categoryService;
    private readonly IPostsService _postsService;

    public CategoriesController(ICategoriesService categoryService, IPostsService postsService)
    {
        _categoryService = categoryService;
        _postsService = postsService;
    }

    // @Note(Avic): What should we return from the controllers? IActionResult, ActionResult<T>
    // or a specific type e.g. List<CategoryDto> ?
    // Answer: https://stackoverflow.com/questions/54432916/asp-net-core-api-actionresultt-vs-async-taskt

    // GET: api/Category
    [HttpGet]
    public async Task<CustomApiResponse> GetCategories([FromQuery] CategoryParameters categoryParameters)
    {
        var categories = await _categoryService.GetCategories(categoryParameters);

        if (categories.Count <= 0)
        {
            // return NotFound();
            return new CustomApiResponse(statusCode: 404, message: "No categories found");
        }

        // return Ok(categories);
        return new CustomApiResponse(categories, new Pagination
        {
            CurrentPage = categories.CurrentPage,
            PageSize = categories.PageSize,
            TotalItemsCount = categories.TotalItemsCount,
            TotalPages = categories.TotalPages,
        });
    }

    // GET: api/Category/5
    [HttpGet("{id}")]
    public async Task<CustomApiResponse> GetCategoryByID(int id)
    {
        var category = await _categoryService.GetCategoryByID(id);

        if (category == null)
        {
            // return NotFound("Category not found");
            return new CustomApiResponse(statusCode: 404, message: "Category not found");
        }

        // return Ok(category);
        return new CustomApiResponse(category);
    }

    // GET: api/Category/posts
    [HttpGet("{id}/posts")]
    public async Task<CustomApiResponse> GetPostsInCategory([FromQuery] PostParameters postParameters, int id)
    {
        var posts = await _postsService.GetPostsInCategory(postParameters, id);

        if (posts == null || posts.Count <= 0)
        {
            // return NotFound("No posts found in category");
            return new CustomApiResponse(message: "No posts found in category", statusCode: 404);
        }

        return new CustomApiResponse(posts, new Pagination
        {
            CurrentPage = posts.CurrentPage,
            PageSize = posts.PageSize,
            TotalItemsCount = posts.TotalItemsCount,
            TotalPages = posts.TotalPages,
        });
    }

    // PUT: api/Category/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<CustomApiResponse> PutCategory(int id, UpdateCategoryDto category)
    {
        var updatedCategory = await _categoryService.UpdateCategory(id, category);

        if (updatedCategory == null)
        {
            // return NotFound("Category not found");
            return new CustomApiResponse(message: "Category not found", statusCode: 404);
        }

        // return Ok(updatedCategory);
        return new CustomApiResponse(updatedCategory);
    }

    // POST: api/Category
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<CustomApiResponse> CreateCategory(CreateCategoryDto category)
    {
        if (category.Title == null || !ModelState.IsValid)
        {
            // return BadRequest(ModelState);
            throw new ApiProblemDetailsException(ModelState);
        }

        var createdCategory = await _categoryService.CreateCategory(category);

        // return CreatedAtAction(nameof(createdCategory), createdCategory);
        return new CustomApiResponse(createdCategory, statusCode: 201);
    }

    // DELETE: api/Category/5
    [HttpDelete("{id}")]
    public async Task<CustomApiResponse> DeleteCategory(int id)
    {
        bool isDeleted = await _categoryService.DeleteCategory(id);

        if (!isDeleted)
        {
            // return NotFound("Category not found");
            return new CustomApiResponse(message: "Category not found", statusCode: 404);
        }

        // return NoContent();
        return new CustomApiResponse(statusCode: 204);
    }
}
