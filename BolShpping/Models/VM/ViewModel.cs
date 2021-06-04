using BolShpping.Models.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolShpping.Models.VM
{
    public class ViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<ProductImage> ProductImages { get; set; }
        public ProductImage ProductImage { get; set; }
        public Category Category { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<SelectListItem> CategoryName { get; set; }
        public string ProductSize { get; set; }

        public string ProductColor { get; set; }

    }
}
