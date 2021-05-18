using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiBlogs.Data;
using ApiBlogs.Dto;
using ApiBlogs.Business;
using Microsoft.AspNetCore.Authorization;

namespace ApiBlogs.Controllers
{
    [Route("post")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PostController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<PostWithIdUserDto>> PostPost(PostDto post)
        {
            var postsBusiness = new PostsBusiness(_context);
            var userBase = _context.Users.SingleOrDefault(p => p.email == User.Identity.Name);
            return await postsBusiness.PostPost(post, userBase.id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostSearchDto>>> GetPosts()
        {
            var postsBusiness = new PostsBusiness(_context);
            return await postsBusiness.GetPosts();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostSearchDto>> GetPost(int id)
        {
            var postsBusiness = new PostsBusiness(_context);
            return await postsBusiness.GetPost(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PostWithIdUserDto>> PutPost(int id, PostDto post)
        {
            var postsBusiness = new PostsBusiness(_context);
            var userBase = _context.Users.SingleOrDefault(p => p.email == User.Identity.Name);
            return await postsBusiness.PutPost(id, userBase.id, post);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var postsBusiness = new PostsBusiness(_context);
            var userBase = _context.Users.SingleOrDefault(p => p.email == User.Identity.Name);
            return await postsBusiness.DeletePost(id, userBase.id);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<PostSearchDto>>> GetPostsSearch([FromQuery] string searchTerm)
        {
            var postsBusiness = new PostsBusiness(_context);
            var userBase = _context.Users.SingleOrDefault(p => p.email == User.Identity.Name);
            return await postsBusiness.GetPosts(searchTerm, userBase.id);
        }
    }
}