using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBlogs.Exceptions
{
    public class UsernotFound : Exception
    {
        public UsernotFound(String message) : base(message)
        {
        }
    }
}
