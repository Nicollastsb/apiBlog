using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBlogs.Exceptions
{
    public class ChangePostNotAuthorizedException : Exception
    {
        public ChangePostNotAuthorizedException(String message) : base(message)
        {
        }
    }
}
