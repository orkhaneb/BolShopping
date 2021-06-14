using BolShpping.Models.DAL;
using BolShpping.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolShopping.ViewComponents
{
    public class CartViewComponent: ViewComponent
    {
        private readonly MyContext _context;
        public CartViewComponent(MyContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var carts = await _context.Carts.Include(c => c.Product).ToListAsync();
            decimal subCategoryPrice = 0;
            foreach (var item in carts)
            {
                subCategoryPrice += item.SubTotalPrice;
            }

            decimal subTotal = carts.Sum(c => c.Product.Price);
            int cartsCount = carts.Count();

            ProductCartViewModel model = new ProductCartViewModel()
            {
                Products = await _context.Products.ToListAsync(),
                Carts = carts,
                SubTotalPrice = subCategoryPrice,
                CartsCount = cartsCount
            };
            return await Task.FromResult((IViewComponentResult)View("Cart", model));
        }
        
}
}
