using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBlogs.Dto
{
    public class UserRegisterDto
    {
        public string displayName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string image { get; set; }
    }
}
