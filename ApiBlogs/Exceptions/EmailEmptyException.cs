using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBlogs.Exceptions
{
    public class EmailEmptyException : Exception
    {
        public EmailEmptyException(String message) : base(message)
        {
        }
    }
}
