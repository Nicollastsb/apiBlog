using ApiBlogs.Data;
using ApiBlogs.Dto;
using ApiBlogs.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApiBlogs.Business
{
    public class ValidationBusiness
    {
        private readonly AppDbContext _context;

        public ValidationBusiness(AppDbContext context)
        {
            _context = context;
        }

        public void ValidationsNewUser(UserRegisterDto user)
        {
            var listErrors400 = new List<String>();

            if (user.displayName.Length < 8)
                listErrors400.Add("\"displayName\"  length must be at least 8 characters long;");
            if(EmailEmpty(user.email))
                listErrors400.Add("\"email\"  is required;");
            else
            {
                if (!EmailValidate(user.email))
                    listErrors400.Add("\"email\"  must be  a valid email;");
            }
            if (user.password == null || user.password == string.Empty)
                listErrors400.Add("\"password\"  is required;");
            else
            {
                if (user.password.Length < 6)
                    listErrors400.Add("\"password\"  length must be at least 6 characters long;");
            }

            if (listErrors400.Count > 0)
            {
                string errorsMessage = string.Empty;
                listErrors400.ForEach(p => { errorsMessage += p + " "; });
                throw new NewUserException(errorsMessage);
            }
        }

        public void ValidationsLogin(UserLoginDto user)
        {
            var listErrors400 = new List<String>();
            if(user.email == null)
                listErrors400.Add("\"email\"  is required;");
            else if (user.email == string.Empty)
                listErrors400.Add("\"email\"  is not allowed to be empty;");

            if (user.password == null)
                listErrors400.Add("\"password\"  is required;");
            else if (PasswordEmpty(user.password))
                listErrors400.Add("\"password\"  is not allowed to be empty;");

            if (listErrors400.Count == 0)
            {
                var userBase = _context.Users.SingleOrDefault(p => p.email == user.email && p.password == user.password);
                if (userBase == null)
                    listErrors400.Add("Campos inválidos");
            }

            if(listErrors400.Count > 0)
            {
                string errorsMessage = string.Empty;
                listErrors400.ForEach(p => { errorsMessage += p + " "; });
                throw new LoginException(errorsMessage);
            }
        }

        public void ValidationsNewPost(PostDto post)
        {
            var listErrors400 = new List<String>();

            if (post.title == null)
                listErrors400.Add("\"title\" is required;");
            if (post.content == null)
                listErrors400.Add("\"content\" is required;");

            if (listErrors400.Count > 0)
            {
                string errorsMessage = string.Empty;
                listErrors400.ForEach(p => { errorsMessage += p + " "; });
                throw new NewPostException(errorsMessage);
            }
        }

        private bool EmailValidate(string email)
        {
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (rg.IsMatch(email))
                return true;            
            else
                return false;
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(e => e.id == id);
        }
        public void EmailExists(string email)
        {
            var teste = _context.Users.Any(e => e.email == email);
            if(teste)
                throw new EmailException("Usuário já existe");
        }

        public bool EmailEmpty(string email)
        {
            if (string.IsNullOrEmpty(email))
                return true;
            else
                return false;
        }

        public bool PasswordEmpty(string password)
        {
            if (password == string.Empty)
                return true;
            else
                return false;
        }
    }
}
