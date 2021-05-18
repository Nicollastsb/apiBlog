using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiBlogs.Data;
using ApiBlogs.Business;
using ApiBlogs.Dto;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace ApiBlogs.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        public IConfiguration _configuration;

        public UserController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserNoPasswordDto>>> GetUsers()
        {
            var _usersBusiness = new UsersBusiness(_context, _configuration);
            return await _usersBusiness.GetUsers();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserNoPasswordDto>> GetUser(int id)
        {
            var teste = User.Identity.Name;
            var _usersBusiness = new UsersBusiness(_context, _configuration);
            return await _usersBusiness.GetUser(id);
        }

        [HttpGet]
        [Route("me")]
        [Authorize]
        public async Task<ActionResult<UserNoPasswordDto>> GetUserLogged()
        {
            var userBase = _context.Users.SingleOrDefault(p => p.email == User.Identity.Name);
            var _usersBusiness = new UsersBusiness(_context, _configuration);
            return await _usersBusiness.GetUser(userBase.id);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<TokenDto>> PostUser(UserRegisterDto user)
        {
            var _usersBusiness = new UsersBusiness(_context, _configuration);
            return await _usersBusiness.PostUser(user);
        }

        [HttpDelete]
        [Route("me")]
        [Authorize]
        public async Task<IActionResult> DeleteUser()
        {
            var userBase = _context.Users.SingleOrDefault(p => p.email == User.Identity.Name);
            var _usersBusiness = new UsersBusiness(_context, _configuration);
            return await _usersBusiness.DeleteUser(userBase);
        }
    }
}
