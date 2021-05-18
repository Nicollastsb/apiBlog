using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBlogs.Dto
{
    public class PostWithIdUserDto
    {
        public string title { get; set; }
        public string content { get; set; }
        public int userId { get; set; }
    }
}
