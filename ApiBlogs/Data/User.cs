using System.Collections.Generic;

namespace ApiBlogs.Data
{
    public class User
    {
        public int id { get; set; }
        public string displayName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string image { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
