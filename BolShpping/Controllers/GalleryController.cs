using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolShpping.Models.BLL;
using BolShpping.Models.DAL;
using BolShpping.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BolShpping.Controllers
{
    public class GalleryController : Controller
    {
        private readonly MyContext _context;

        public GalleryController(MyContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            var products = await _context.Products.ToListAsync();

            ViewModel vm = new ViewModel()
            {
                Categories = categories,
                Products = products
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<JsonResult> Filter(int id, int sizeID, int fromPrice, int toPrice)
        {
            if (id == 0 && sizeID == 0)
            {
                return Json(new
                {
                    status = 404
                });
            }

            List<Product> filter = null;

            var category = await _context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
            var size = _context.Products.Where(p => p.Id == sizeID).FirstOrDefault();

            var productImages = await _context.ProductImages.ToListAsync() ;


            if (category == null)
            {
                filter = await _context.Products.Where(p => p.Size == size.Size).ToListAsync();
            }

            if (size == null)
            {
                filter = await _context.Products.Where(
                    
                    p => (p.CategoryId == category.Id)).ToListAsync();
            }

            if (category != null && size != null)
            {
                filter = await _context.Products.Where(
                    
                                    p => (p.CategoryId == category.Id && p.Size == size.Size)).ToListAsync();
            }


            return Json(new
            {
                status = 200,
                filterInfo = filter,

                productImages = productImages

            });
        }
    }
}
