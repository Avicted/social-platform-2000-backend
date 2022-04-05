using Microsoft.AspNetCore.Mvc;
using social_platform_2000_backend.Models;
using social_platform_2000_backend.Services;
using social_platform_2000_backend.DTO;

namespace social_platform_2000_backend.Controllers
{
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

        // GET: api/Category
        [HttpGet]
        public async Task<IActionResult> GetCategories(int? pageNumber)
        {
            var categories = await _categoryService.GetCategories(pageNumber);

            return Ok(categories);
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByID(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // GET: api/Category/posts
        [HttpGet("{id}/posts")]
        public async Task<CustomApiResponse> GetPostsInCategory(int id, int? pageNumber)
        {
            var posts = await _postsService.GetPostsInCategory(id, pageNumber);

            if (posts == null || posts.Pagination.TotalItemsCount <= 0)
            {
                return new CustomApiResponse(
                    NoContent(),
                    "No posts found",
                    404
                );
            }

            return posts;
        }

        // PUT: api/Category/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, UpdateCategoryDto category)
        {
            var updatedCategory = await _categoryService.UpdateCategory(id, category);

            if (updatedCategory == null)
            {
                return NotFound();
            }

            return Ok(updatedCategory);
        }

        // POST: api/Category
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> PostCategory(CreateCategoryDto category)
        {
            if (category.Title == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdCategory = await _categoryService.CreateCategory(category);
            return createdCategory;
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            bool isDeleted = await _categoryService.DeleteCategory(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}