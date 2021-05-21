using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolShpping.Models.BLL
{
    public class BlogImage
    {
        public int Id { get; set; }
        public string ImageCode { get; set; }

        //foreign key connection
        public int BlogId { get; set; }

        public virtual Blog Blog { get; set; }
    }
}
