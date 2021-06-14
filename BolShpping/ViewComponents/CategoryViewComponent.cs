using BolShpping.Models.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolShpping.ViewComponents
{
    public class CategoryViewComponent: ViewComponent
    {
        private readonly MyContext _context;
        public CategoryViewComponent(MyContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var carts = await _context.Categories.ToListAsync();
           
            return await Task.FromResult((IViewComponentResult)View("Category", carts));
        }
    }
}
