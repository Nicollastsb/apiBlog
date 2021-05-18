using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBlogs.Exceptions
{
    public class EmailException : Exception
    {
        public EmailException(String message) : base(message)
        {
        }
    }
}
