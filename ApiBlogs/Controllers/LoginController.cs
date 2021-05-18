using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiBlogs.Data;
using ApiBlogs.Dto;
using ApiBlogs.Business;
using Microsoft.Extensions.Configuration;

namespace ApiBlogs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;
        public IConfiguration _configuration;

        public LoginController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<TokenDto>> PostLogin(UserLoginDto user)
        {
            var loginBusiness = new LoginBusiness(_context, _configuration);
            return await loginBusiness.LoginUser(user);
        }
    }
}
