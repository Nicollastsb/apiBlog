using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBlogs.Exceptions
{
    public class PostNotFound : Exception
    {
        public PostNotFound(String message) : base(message)
        {
        }
    }
}
