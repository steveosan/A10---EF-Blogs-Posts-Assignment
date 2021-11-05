using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_inclass_10
{

    public class Post
    {
        public int Postid { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }

        //entity framework relationship
        public Blog Blog { get; set; }
    }
}