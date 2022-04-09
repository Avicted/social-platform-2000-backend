using Microsoft.AspNetCore.Mvc;
using sp2000.Models;
using sp2000.Services;
using sp2000.Application.DTO;

namespace sp2000.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _postsService;

        public PostsController(IPostsService postsService)
        {
            _postsService = postsService;
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postsService.GetPostByID(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, UpdatePostDto post)
        {
            var updatedPost = await _postsService.UpdatePost(id, post);

            if (updatedPost == null)
            {
                return NotFound();
            }

            return Ok(updatedPost);
        }

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostDto post)
        {
            var createdPost = await _postsService.CreatePost(post);

            if (createdPost == null)
            {
                return NotFound();
            }

            return Ok(createdPost);

        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            bool isDeleted = await _postsService.DeletePost(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
