using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolShpping.Models.BLL
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public string Color { get; set; }

        //foreign key connection
        public int CategoryId { get; set; }
        public int AppUserId { get; set; }

        public virtual Category Category { get; set; }
        public virtual AppUser AppUser { get; set; }

        public IEnumerable<ProductImage> ProductImages { get; set; }
    }
}
