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
    public class AboutController : Controller
    {
        private readonly MyContext _context;
        public readonly IWebHostEnvironment _env;

        public AboutController(MyContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        //About index Function Start
        public async Task<IActionResult> Index()
        {
            return View(await _context.Abouts.ToListAsync());
        }
        //About index Function End

        //About Create Function Start
        public IActionResult Create()
        {
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, About about )
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    about.ImageCode = await ImagesHelpers.ImageUploadAsync(_env.WebRootPath, file, "img", "about");
                }

                var result = await _context.Abouts.AddAsync(about);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        //About Create Function End

        //About Edit Function Start
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var info = await _context.Abouts.FirstOrDefaultAsync(x => x.Id == id);

            if (info == null)
            {
                return NotFound();
            }
            return View(info);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile file, int id, About about )
        {
            if (id != about.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(about);
            }
            if (file != null)
            {
                if (about.ImageCode != null && about.ImageCode != string.Empty)
                {
                    ImagesHelpers.DeleteImage(about.ImageCode, "img/about/");
                }
                about.ImageCode = await ImagesHelpers.ImageUploadAsync(_env.WebRootPath, file, "img", "about");

            }
            _context.Update(about);
            await _context.SaveChangesAsync();

            var ProAbout = await _context.Abouts.FindAsync(id);

            ProAbout.Title = about.Title;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        //About Edit Function End

        //About Delete Function Start
        public async Task<IActionResult> Delete(int id)
        {
            var Proabout = await _context.Abouts.FindAsync(id);
            _context.Abouts.Remove(Proabout);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //About Delete Function End
    }
}
