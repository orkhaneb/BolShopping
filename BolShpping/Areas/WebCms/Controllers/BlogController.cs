using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolShpping.Extensions;
using BolShpping.Models.BLL;
using BolShpping.Models.DAL;
using BolShpping.Models.VM;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BolShpping.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    [Route("WebCms/[controller]/[action]")]
    public class BlogController : Controller
    {
        public readonly MyContext _context;
        public readonly IWebHostEnvironment _env;


        public BlogController(MyContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }
        //Blog index Function Start
        public async Task<IActionResult> Index()
        {
            var blog = await _context.Blogs.Include(i => i.BlogImages).ToListAsync();
            return View(blog);
        }
        //Blog index Function End

        // Blog Create Function Start
        public IActionResult Create()
        {
            return View();
        }
        //Post section
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog, IFormFile file)
        {
            if (ModelState.IsValid)
            {

                var ProBlog = await _context.Blogs.AddAsync(blog);
                await _context.SaveChangesAsync();

                if (file != null)
                {
                    var image = await ImagesHelpers.ImageUploadAsync(_env.WebRootPath, file, "img", "blog");

                    await _context.BlogImages.AddAsync(new BlogImage
                    {
                        BlogId = blog.Id,
                        ImageCode = image
                    });
                } 

                    await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        // Blog Create Function End

        // Blog Edit Function Start
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var ProBlog = await _context.Blogs.FindAsync(Id);

            ViewModel vm = new ViewModel();

            vm.Blog = ProBlog;

            vm.BlogImages = _context.BlogImages.Where(i => i.BlogId == Id).ToList();

            return View(vm);
        }
        //Post section
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ViewModel viewModel, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                Blog blog = await _context.Blogs.FindAsync(viewModel.Blog.Id);

                if (file!= null)
                {

                    List<BlogImage> images = await _context.BlogImages.Where(i => i.BlogId == viewModel.Blog.Id).ToListAsync();

                    foreach (var item in images)
                    {
                        if (item.ImageCode != null && item.ImageCode != string.Empty)
                        {
                            ImagesHelpers.DeleteImage(item.ImageCode, "img/blog/");
                        }
                    }
                    _context.BlogImages.RemoveRange(images);
                }

           
                    var image = await ImagesHelpers.ImageUploadAsync(_env.WebRootPath, file, "img", "blog");
                    await _context.BlogImages.AddAsync(new BlogImage
                    {
                        BlogId = viewModel.Blog.Id,
                        ImageCode = image
                    });
                    await _context.SaveChangesAsync();
                
                {
                    blog.Title = viewModel.Blog.Title;
                    blog.Description = viewModel.Blog.Description;
                    blog.DateTime = viewModel.Blog.DateTime;
                    blog.UserName = viewModel.Blog.UserName;

                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            return View(viewModel);
        }
        // Blog Edit Function End

        // Blog Function Start
        public async Task<IActionResult> Delete(int id)
        {
            var ProBlog = await _context.Blogs.FindAsync(id);

            List<BlogImage> images = _context.BlogImages.Where(i => i.BlogId == ProBlog.Id).ToList();

            foreach (var item in images)
            {
                if (item.ImageCode != null && item.ImageCode != string.Empty)
                {
                    ImagesHelpers.DeleteImage(item.ImageCode, "img/blog/");
                }
            }
            _context.BlogImages.RemoveRange(images);
            await _context.SaveChangesAsync();

            _context.Blogs.Remove(ProBlog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Blog Function End
    }
}
