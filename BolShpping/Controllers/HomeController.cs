using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BolShpping.Models;
using BolShpping.Models.VM;
using BolShpping.Models.DAL;
using Microsoft.EntityFrameworkCore;
using BolShpping.Models.BLL;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BolShpping.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyContext _context;


        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var count = await _context.Products.ToListAsync();
            ViewModel vm = new ViewModel()
            {
                Products = await _context.Products.Include(p => p.ProductImages).Include(p => p.Category).ToListAsync(),
                Categories = await _context.Categories.ToListAsync()
            };
            return View(vm);
        }

        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> AddBasket(int id, int quantity = 1)
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

        public async Task<IActionResult> AddFavourite(int id)
        {
            List<Favourite> carts = new List<Favourite>();

            var session = HttpContext.Session.GetString("ShoppingCart");

            if (session != null)
            {
                carts = JsonConvert.DeserializeObject<List<Favourite>>(session);

                if (carts.Any(c => c.ProductId == id))
                {
                    Favourite addCart = carts.First(c => c.ProductId == id);
                    addCart.Count++;
                }
                else
                {
                    var product = await _context.Products.FindAsync(id);
                    carts.Add(new Favourite
                    {
                        Product = product,
                        ProductId = id,
                        SubTotalPrice =  product.Price,
                        Count = 1
                    });
                }
            }
            else
            {
                var product = await _context.Products.FindAsync(id);
                carts.Add(new Favourite
                {
                    Product = product,
                    ProductId = id,
                    SubTotalPrice = product.Price,
                    Count = 1
                });
            }
            int cartCount = 0;
            for (int i = 0; i < carts.Count; i++)
            {
                cartCount += carts[i].Count;
            }

            string jsonList = JsonConvert.SerializeObject(carts);
            HttpContext.Session.SetString("ShoppingFavourite", jsonList);

            //int cartCount = 0;
            //ViewBag.Count = carts.Sum(c => c.Count);

            return Json(new
            {
                status = 200,
                message = "Data was successfully added!",
                dataCount = cartCount

            });
        }
    }
}
