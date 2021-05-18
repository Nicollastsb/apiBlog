using ApiBlogs.Data;
using ApiBlogs.Dto;
using ApiBlogs.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBlogs.Business
{
    public class UsersBusiness : ControllerBase
    {
        private readonly AppDbContext _context;
        public IConfiguration _configuration;

        public UsersBusiness(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ActionResult<IEnumerable<UserNoPasswordDto>>> GetUsers()
        {
            var userList = await _context.Users.ToListAsync();
            var userNoPasswordlist = userList.Select(x => new UserNoPasswordDto
                                        {
                                            id = x.id,
                                            displayName = x.displayName,
                                            email = x.email,
                                            image = x.image
                                        }).ToList();
            return userNoPasswordlist;
        }

        public async Task<ActionResult<UserNoPasswordDto>> GetUser(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if(user == null)
                    throw new UsernotFound("Usuário não existe");
                else
                {
                    var userNoPassword = new UserNoPasswordDto();
                    userNoPassword.id = user.id;
                    userNoPassword.displayName = user.displayName;
                    userNoPassword.email = user.email;
                    userNoPassword.image = user.image;
                    return userNoPassword;
                }
            }
            catch(UsernotFound e)
            {
                return StatusCode(404, e.Message);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        public async Task<ActionResult<TokenDto>> PostUser(UserRegisterDto user)
        {
            try
            {
                var validationBusiness = new ValidationBusiness(_context);
                validationBusiness.ValidationsNewUser(user);
                validationBusiness.EmailExists(user.email);

                var newUser = new User();
                newUser.displayName = user.displayName;
                newUser.email = user.email;
                newUser.password = user.password;
                newUser.image = user.image;
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                var tokenBusiness = new TokenBusiness(_configuration);
                var newToken = new TokenDto();
                newToken.token = tokenBusiness.CreateToken(newUser);
                return newToken;
            }
            catch(NewUserException e)
            {
                return StatusCode(400, e.Message);
            }
            catch(EmailException e)
            {
                return StatusCode(409, e.Message);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        public async Task<IActionResult> DeleteUser(User user)
        {            
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return StatusCode(204);
        }
    }
}
