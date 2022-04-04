using Microsoft.AspNetCore.Mvc;
using social_platform_2000_backend.Models;
using social_platform_2000_backend.Services;

namespace social_platform_2000_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postsService;

        public PostsController(IPostService postsService)
        {
            _postsService = postsService;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts(int categoryId)
        {
            // return await _postsService.GetPosts();
            return await _postsService.GetPostsInCategory(categoryId);
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
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
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            if (id != post.PostId)
            {
                return BadRequest();
            }

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
        public async Task<ActionResult<Post>> CreatePost(CreatePostVM post)
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
