using Microsoft.AspNetCore.Mvc;
using social_platform_2000_backend.Models;
using social_platform_2000_backend.Services;
using social_platform_2000_backend.ViewModels;

namespace social_platform_2000_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryVM>>> GetCategories()
        {
            return await _categoryService.GetCategories();
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryVM>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByID(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Category/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

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
        public async Task<ActionResult<CategoryVM>> PostCategory(CreateCategoryVM category)
        {
            var createdCategory = await _categoryService.CreateCategory(category);
            return createdCategory;
            // return CreatedAtAction("GetCategory", new { id = createdCategory.CategoryId }, category);
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
