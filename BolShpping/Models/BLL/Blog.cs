using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolShpping.Models.BLL
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public int CountOfViews { get; set; }

        //primary key connection
        public virtual ICollection<BlogImage> BlogImages { get; set; }
    }
}
