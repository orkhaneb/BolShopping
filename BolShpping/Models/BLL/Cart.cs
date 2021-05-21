using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolShpping.Models.BLL
{
    public class Cart
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotalPrice { get; set; }

        //foreign key connection
        public int ProductId { get; set; }
        public int UserId { get; set; }

        public virtual Product Product { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
