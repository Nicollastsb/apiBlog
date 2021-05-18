using ApiBlogs.Data;
using ApiBlogs.Dto;
using ApiBlogs.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiBlogs.Business
{
    public class LoginBusiness : ControllerBase
    {
        private readonly AppDbContext _context;
        public IConfiguration _configuration;

        public LoginBusiness(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ActionResult<TokenDto>> LoginUser(UserLoginDto user)
        {
            try
            {
                var validationBusiness = new ValidationBusiness(_context);
                validationBusiness.ValidationsLogin(user);
                var userBase = _context.Users.FirstOrDefault(p=>p.email == user.email);

                var tokenBusiness = new TokenBusiness(_configuration);
                var newToken = new TokenDto();
                newToken.token = tokenBusiness.CreateToken(userBase);
                return newToken;
            }
            catch (LoginException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
