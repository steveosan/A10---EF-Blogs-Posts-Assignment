using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_inclass_10
{
    public class Blog 
    {
        public int BlogId {set; get;}
        public string Name { get; set; }
        
        //entity framework relationship
        public List<Post> Posts { get; set; }
    }

}