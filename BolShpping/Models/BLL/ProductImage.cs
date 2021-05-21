using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolShpping.Models.BLL
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string ImageCode { get; set; }

        //foreign key connection
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
