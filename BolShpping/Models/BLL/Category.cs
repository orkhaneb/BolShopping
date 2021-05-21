using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolShpping.Models.BLL
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageCode { get; set; }

        //primary key connection
        public virtual ICollection<Product> Products { get; set; }
    }
}
