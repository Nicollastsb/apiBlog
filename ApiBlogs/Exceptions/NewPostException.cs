using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBlogs.Exceptions
{
    public class NewPostException : Exception
    {
        public NewPostException(String message) : base(message)
        {
        }
    }
}
