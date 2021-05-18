using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBlogs.Exceptions
{
    public class DeletePostNotAuthorized : Exception
    {
        public DeletePostNotAuthorized(String message) : base(message)
        {
        }
    }
}
