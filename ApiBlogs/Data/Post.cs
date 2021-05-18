using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBlogs.Data
{
    public class Post
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public int userId { get; set; }
        public DateTime published { get; set; }
        public DateTime updated { get; set; }

        public User User { get; set; }
    }
}
