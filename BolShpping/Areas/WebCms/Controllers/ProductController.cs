using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolShpping.Models.BLL;
using BolShpping.Models.DAL;
using BolShpping.Models.VM;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BolShpping.Extensions;


namespace BolShpping.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    [Route("WebCms/[controller]/[action]")]
    public class ProductController : Controller
    {
        private readonly MyContext _context;
        public readonly IWebHostEnvironment _env;
        public ProductController(MyContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        // Product Index Function Start
        public async Task<IActionResult> Index()
        {
            var product = await _context.Products.Include(i => i.ProductImages).Include(c => c.Category).ToListAsync();
            return View(product);
        }
        // Product Index Function End

        // Product Create Function Start
        public IActionResult Create()
        {
            ViewModel vm = new ViewModel()
            {
                CategoryName = _context.Categories.Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() }),
                Product = new Product()
            };
            return View(vm);
        }
        //Post section
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ViewModel viewModel, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                var Catagory = await _context.Categories.FindAsync(viewModel.Category.Id);
                Product model = new Product()
                {
                    Name = viewModel.Product.Name,
                    Description = viewModel.Product.Description,
                    CategoryId = Catagory.Id,
                    Color = viewModel.ProductColor,
                    Size = viewModel.ProductSize,
                    DiscountPrice = viewModel.Product.DiscountPrice,
                    Price = viewModel.Product.Price,
                };

                await _context.Products.AddAsync(model);

                await _context.SaveChangesAsync();
                int count = 0;
                foreach (var item in files)
                {
                    var image = await ImagesHelpers.ImageUploadAsync(_env.WebRootPath, item, "img", "product");
                    await _context.ProductImages.AddAsync(new ProductImage
                    {
                        ProductId = model.Id,
                        ImageCode = image
                    });
                    if (count < files.Count())
                    {
                        model.ProductImages[count].ImageCode = image;
                        ++count;
                    }    

                    await _context.SaveChangesAsync();
                }

            }

            return RedirectToAction(nameof(Index));
        }
        // Product Create Function End

        // Product Edit Function Start
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var model = await _context.Products.FindAsync(Id);
            
            ViewModel vm = new ViewModel();

            vm.Categories = await _context.Categories.ToListAsync();

            vm.CategoryName = _context.Categories.Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString(), Selected = c.Id == model.CategoryId });

            vm.Product = model;
            
            vm.ProductImages = _context.ProductImages.Where(i => i.ProductId == Id).ToList();

            return View(vm);
        }
        //Post section

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( ViewModel viewModel, List<IFormFile> file)
        {
            if (ModelState.IsValid)
            {
                Product product = await _context.Products.FindAsync(viewModel.Product.Id);

                if (file.Count != 0)
                {

                List<ProductImage> images = await _context.ProductImages.Where(i => i.ProductId == viewModel.Product.Id).ToListAsync();

                foreach (var item in images)
                {
                    if (item.ImageCode != null && item.ImageCode != string.Empty)
                    {
                        ImagesHelpers.DeleteImage(item.ImageCode, "img/product/");
                    }
                }
                _context.ProductImages.RemoveRange(images);
                }
               
                    foreach (var item in file)
                    {
                        var image = await ImagesHelpers.ImageUploadAsync(_env.WebRootPath, item, "img", "product");
                        await _context.ProductImages.AddAsync(new ProductImage
                        {
                            ProductId = viewModel.Product.Id,
                            ImageCode = image
                        });
                        await _context.SaveChangesAsync();
                    }
                {
                    product.Name = viewModel.Product.Name;
                    product.Description = viewModel.Product.Description;
                    product.CategoryId = viewModel.Category.Id;
                    product.Color = viewModel.ProductColor;
                    product.Size = viewModel.ProductSize;
                    product.DiscountPrice = viewModel.Product.DiscountPrice;
                    product.Price = viewModel.Product.Price;

                    await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
                }
            }
            return View(viewModel);
        }
        // Product Edit Function End

        // Delete Function Start
        public async Task<IActionResult> Delete(int id)
        {
            var Produc = await _context.Products.FindAsync(id);

            List<ProductImage> images = _context.ProductImages.Where(i => i.ProductId == Produc.Id).ToList();

            foreach (var item in images)
            {
            if (item.ImageCode != null && item.ImageCode != string.Empty)
            {
                ImagesHelpers.DeleteImage(item.ImageCode, "img/product/");
            }
            }
            _context.ProductImages.RemoveRange(images);
            await _context.SaveChangesAsync();

            _context.Products.Remove(Produc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Delete Function End


    }
}
