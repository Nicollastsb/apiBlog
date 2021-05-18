using System;

namespace ApiBlogs.Exceptions
{
    public class NewUserException : Exception
    {
        public NewUserException(String message) : base(message)
        {
        }
    }
}
