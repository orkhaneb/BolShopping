using BolShpping.Models.BLL;
using BolShpping.Models.DAL;
using BolShpping.Models.VM;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;

        public ProductDetailController(MyContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(int id)
        {
            var singleProduct = await _context.Products.FindAsync(id);

            ViewModel vm = new ViewModel()
            {
                Product = singleProduct,
                Products = await _context.Products.Where(p => p.CategoryId == singleProduct.CategoryId).ToListAsync(),
                Category = new Category()
            };

            var modelid = _context.ProductImages?.Where(pi => pi.ProductId == singleProduct.Id).FirstOrDefault().ImageCode;
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddBasket(int id, int quantity)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new
                {
                    status = 400
                });
            }
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
                string appUserId = _userManager.GetUserId(HttpContext.User);
                cart = new Cart()
                {
                    Product = product,
                    ProductId = id,
                    Quantity = quantity,
                    SubTotalPrice = productQuantity,
                    AppUserId = appUserId
                    
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
