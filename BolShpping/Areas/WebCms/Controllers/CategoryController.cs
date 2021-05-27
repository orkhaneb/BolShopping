using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolShpping.Extensions;
using BolShpping.Models.BLL;
using BolShpping.Models.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BolShpping.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    [Route("WebCms/[controller]/[action]")]
    public class CategoryController : Controller
    {
        private readonly MyContext _context;
        public readonly IWebHostEnvironment _env;

        public CategoryController(MyContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        //Category Index Function Start
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }
        //Category Index Function End

        //Category Create Function Start
        public IActionResult Create()
        {
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, Category cate)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    cate.ImageCode = await ImagesHelpers.ImageUploadAsync(_env.WebRootPath, file, "img", "category");
                }

                var result = await _context.Categories.AddAsync(cate);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        //Category Create Function End

        //Category Edit Function Start
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Brand = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (Brand == null)
            {
                return NotFound();
            }
            return View(Brand);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile file, int id, Category cate)
        {
            if (id != cate.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(cate);
            }
            if (file != null)
            {
                if (cate.ImageCode != null && cate.ImageCode != string.Empty)
                {
                    ImagesHelpers.DeleteImage(cate.ImageCode, "img/category/");
                }
                cate.ImageCode = await ImagesHelpers.ImageUploadAsync(_env.WebRootPath, file, "img", "category");

            }
            _context.Update(cate);
            await _context.SaveChangesAsync();

            var ProCategory = await _context.Categories.FindAsync(id);

            ProCategory.Name = cate.Name;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        //Category Edit Function End

        //Category Delete Function Start
        public async Task<IActionResult> Delete(int id)
        {
            var ProBrand = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(ProBrand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //Category Delete Function End
    }
}
