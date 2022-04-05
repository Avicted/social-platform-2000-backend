using Microsoft.AspNetCore.Mvc;
using social_platform_2000_backend.Models;
using social_platform_2000_backend.Services;
using social_platform_2000_backend.DTO;

namespace social_platform_2000_backend.Controllers
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
        public async Task<ActionResult<PostDto>> GetPost(int id)
        {
            var post = await _postsService.GetPostByID(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
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
        public async Task<ActionResult<PostDto>> CreatePost(CreatePostDto post)
        {
            var createdPost = await _postsService.CreatePost(post);

            if (createdPost == null)
            {
                return NotFound();
            }

            return createdPost;

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
