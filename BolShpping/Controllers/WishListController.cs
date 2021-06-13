using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolShpping.Models.BLL;
using BolShpping.Models.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BolShpping.Controllers
{
    public class WishListController : Controller
    {
        private readonly MyContext _context;

        public WishListController( MyContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Favourite> cart = new List<Favourite>();

            var session = HttpContext.Session.GetString("ShoppingFavourite");

            if (session != null)
            {
                cart = JsonConvert.DeserializeObject<List<Favourite>>(session);
            }

            return View(cart);
        }


        [HttpPost]
        public async Task<IActionResult> AddBasket(int id, int quantity = 1)
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

        //[HttpPost]
        //public IActionResult RemoveBasket(int id)
        //{

        //    var session = HttpContext.Session.GetString("ShoppingFavourite");
        //    if (session != null)
        //    {
        //        _httpContext.Session.Remove(session);
        //        return Json(new
        //        {
        //            status = 200
        //        });
        //    }
        //    //var cart = await _context.Carts.FindAsync(id);

        //    //_context.Carts.Remove(cart);
        //    //await _context.SaveChangesAsync();
        //    return Json(new
        //    {
        //        status = 400
        //    });

        //}
    }
}
