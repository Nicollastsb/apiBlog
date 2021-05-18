using ApiBlogs.Data;
using ApiBlogs.Dto;
using ApiBlogs.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBlogs.Business
{
    public class PostsBusiness : ControllerBase
    {
        private readonly AppDbContext _context;

        public PostsBusiness(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<PostWithIdUserDto>> PostPost(PostDto post, int UserId)
        {
            var validationBusiness = new ValidationBusiness(_context);
            validationBusiness.ValidationsNewPost(post);
            var postFull = new Post();
            postFull.title = post.title;
            postFull.content = post.content;
            postFull.userId = UserId;
            postFull.published = DateTime.Now;
            _context.Posts.Add(postFull);
            await _context.SaveChangesAsync();

            var NewPost = new PostWithIdUserDto();
            NewPost.title = postFull.title;
            NewPost.content = postFull.content;
            NewPost.userId = postFull.userId;
            return NewPost;
        }

        public async Task<ActionResult<IEnumerable<PostSearchDto>>> GetPosts()
        {
            var posts = await _context.Posts.Include("User").ToListAsync();
            var postList = posts.Select(x => new PostSearchDto
            {
                id = x.id,
                title = x.title,
                content = x.content,
                published = x.published,
                updated = x.updated,
                User = new UserNoPasswordDto()
                {
                    id = x.User.id,
                    displayName = x.User.displayName,
                    email = x.User.email,
                    image = x.User.image,
                }
            }).ToList();
            return postList;
        }

        // GET: api/Posts/5
        public async Task<ActionResult<PostSearchDto>> GetPost(int id)
        {
            try
            {
                var post = await _context.Posts.FindAsync(id);

                if (post == null)
                    throw new PostNotFound("Post não existe");
                else
                {
                    var user = await _context.Users.FindAsync(post.userId);
                    var postBase = new PostSearchDto
                    {
                        id = post.id,
                        title = post.title,
                        content = post.content,
                        published = post.published,
                        updated = post.updated,
                        User = new UserNoPasswordDto()
                        {
                            id = user.id,
                            displayName = user.displayName,
                            email = user.email,
                            image = user.image,
                        }
                    };

                    return postBase;
                }
            }
            catch (PostNotFound e)
            {
                return StatusCode(404, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        public async Task<ActionResult<PostWithIdUserDto>> PutPost(int id, int userId, PostDto post)
        {
            try
            {
                var postsUser = _context.Posts.Where(p=>p.userId == userId).ToList();
                if(!postsUser.Exists(p=>p.id == id))
                    throw new ChangePostNotAuthorizedException("Usuário não autorizado;");

                var validationBusiness = new ValidationBusiness(_context);
                validationBusiness.ValidationsNewPost(post);

                var postChanged = await _context.Posts.FindAsync(id);
                if(postChanged != null)
                {
                    postChanged.title = post.title;
                    postChanged.content = post.content;
                    postChanged.updated = DateTime.Now;
                    _context.Entry(postChanged).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    var postReturn = new PostWithIdUserDto();
                    postReturn.title = postChanged.title;
                    postReturn.content = postChanged.content;
                    postReturn.userId = postChanged.userId;
                    return postReturn;
                }
                else
                {
                    throw new ChangePostNotAuthorizedException("Usuário não autorizado;");
                }
                
            }
            catch (ChangePostNotAuthorizedException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (NewPostException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        public async Task<IActionResult> DeletePost(int id, int userId)
        {
            try
            {
                var post = await _context.Posts.FindAsync(id);
                if (post == null)
                    throw new PostNotFound("Post não existe;");

                var postsUser = _context.Posts.Where(p => p.userId == userId).ToList();
                if (!postsUser.Exists(p => p.id == id))
                    throw new DeletePostNotAuthorized("Usuário não autorizado;");

                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
                return StatusCode(204);
            }
            catch (DeletePostNotAuthorized e)
            {
                return StatusCode(401, e.Message);
            }
            catch (PostNotFound e)
            {
                return StatusCode(404, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        public async Task<ActionResult<IEnumerable<PostSearchDto>>> GetPosts(string searchTerm, int userId)
        {
            searchTerm = searchTerm ?? "";

            var posts = _context.Posts.Include("User")
                .Where(p => p.userId == userId && (p.title.ToLower().Contains(searchTerm.ToLower()) || p.content.ToLower().Contains(searchTerm.ToLower()))).ToList();

            var postList = posts.Select(x => new PostSearchDto
            {
                id = x.id,
                title = x.title,
                content = x.content,
                published = x.published,
                updated = x.updated,
                User = new UserNoPasswordDto()
                {
                    id = x.User.id,
                    displayName = x.User.displayName,
                    email = x.User.email,
                    image = x.User.image,
                }
            }).ToList();
            return postList;
        }
    }
}
