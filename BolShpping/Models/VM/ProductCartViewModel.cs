using BolShpping.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolShpping.Models.VM
{
    public class ProductCartViewModel
    {
        public IList<Product> Products { get; set; }

        public IList<Cart> Carts { get; set; }
        public Cart Cart { get; set; }

        public decimal SubTotalPrice { get; set; }

        public int CartsCount { get; set; }
        public int Quantity { get; set; }



    }
}
