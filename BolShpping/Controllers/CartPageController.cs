using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolShpping.Models.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BolShpping.Models.VM;

namespace BolShpping.Controllers
{
    public class CartPageController : Controller
    {
        private readonly MyContext _context;

        public CartPageController(MyContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var carts = await _context.Carts.Include(c => c.Product).ToListAsync();
            decimal subCategoryPrice = 0;
            foreach (var item in carts)
            {
                subCategoryPrice += item.SubTotalPrice;
            }

            decimal subTotal = carts.Sum(c => c.Product.Price);
            int cartsCount = carts.Count();

            ProductCartViewModel vm = new ProductCartViewModel()
            {
                Products = await _context.Products.ToListAsync(),
                Carts = carts,
                SubTotalPrice = subCategoryPrice,
                CartsCount = cartsCount
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ClearBasket()
        {
            var carts = await _context.Carts.ToListAsync();

            foreach (var cart in carts)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }


            return Json(new
            {
                status = 200

            });
        }

    }
}
