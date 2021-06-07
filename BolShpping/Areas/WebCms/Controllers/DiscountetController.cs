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
    public class DiscountetController : Controller
    {
        private readonly MyContext _context;
        public readonly IWebHostEnvironment _env;

        public DiscountetController(MyContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        // Discountet index Function Start
        public async Task<IActionResult> Index()
        {
            return View(await _context.Discountends.ToListAsync());
        }
        // Discountet index Function End

        // Discountet Create Function Start
        public IActionResult Create()
        {
            return View();
        }

        //Post section

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, Discountend discountet)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    discountet.ImageCod = await ImagesHelpers.ImageUploadAsync(_env.WebRootPath, file, "img", "discountet");
                }
                _context.Add(discountet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(discountet);
        }
        // Discountet Create Function End

        // Discountet Edit Function Start
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountet = await _context.Discountends.FindAsync(id);
            if (discountet == null)
            {
                return NotFound();
            }
            return View(discountet);
        }

        //Post section

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile file, Discountend discountet)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        if (discountet.ImageCod != null && discountet.ImageCod != string.Empty)
                        {
                            ImagesHelpers.DeleteImage(discountet.ImageCod, "img/discountet/");
                        }
                        discountet.ImageCod = await ImagesHelpers.ImageUploadAsync(_env.WebRootPath, file, "img", "discountet");

                    }
                    _context.Update(discountet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscountetExists(discountet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(discountet);
        }

        private bool DiscountetExists(int id)
        {
            return _context.Discountends.Any(e => e.Id == id);
        }
        // Discountet Edit Function End

        // Discountet Delete Function Start
        public async Task<IActionResult> Delete(int id)
        {
            var discountet = await _context.Discountends.FindAsync(id);
            if (discountet.ImageCod != null && discountet.ImageCod != string.Empty)
            {
                ImagesHelpers.DeleteImage(discountet.ImageCod, "img/discountet/");
            }
            _context.Discountends.Remove(discountet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Discountet Delete Function End
    }
}
