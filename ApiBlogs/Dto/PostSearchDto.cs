using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBlogs.Dto
{
    public class PostSearchDto
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime published { get; set; }
        public DateTime updated { get; set; }

        public UserNoPasswordDto User { get; set; }
    }
}
