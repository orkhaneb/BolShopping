using BolShpping.Models.BLL;
using BolShpping.Models.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolShpping.Controllers
{
    public class ProductDetailController : Controller
    {
        public readonly MyContext _context;
        public ProductDetailController(MyContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int id)
        {
            var singleProduct = await _context.Products.FindAsync(id);

            var modelid = _context.ProductImages?.Where(pi => pi.ProductId == singleProduct.Id).FirstOrDefault().ImageCode;
            return View(singleProduct);
        }

        [HttpPost]
        public async Task<IActionResult> AddBasket(int id, int quantity)
        {
            Cart cart = null;
            if (id != 0)
            {
                var carts = await _context.Carts.ToListAsync();

                foreach (var item in carts)
                {
                    if (item.ProductId == id)
                    {
                        return Json(new
                        {
                            status = 404,
                            basket = item
                        });
                    }
                }
                var product = await _context.Products.FindAsync(id);
                var productQuantity = quantity * product.Price;
                cart = new Cart()
                {
                    Product = product,
                    ProductId = id,
                    Quantity = quantity,
                    SubTotalPrice = productQuantity
                };
                await _context.Carts.AddAsync(cart);
                await _context.SaveChangesAsync();
            }

            return Json(
                new
                {
                    basket = cart,
                    status = 200

                }
            );
        }
    }
}
