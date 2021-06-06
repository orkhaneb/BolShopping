using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolShpping.Models.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BolShpping.Controllers
{
    public class WishListController : Controller
    {
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
    }
}
