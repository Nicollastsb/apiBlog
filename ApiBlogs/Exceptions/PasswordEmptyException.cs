using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBlogs.Exceptions
{
    public class PasswordEmptyException : Exception
    {
        public PasswordEmptyException(String message) : base(message)
        {
        }
    }
}
