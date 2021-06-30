using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolShpping.Models.DAL;
using BolShpping.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BolShpping.Controllers
{
    public class AboutController : Controller
    {
        private readonly MyContext _context;

        public AboutController(MyContext context)
        {
            _context = context;
        }
        //About index Function Start
        public async Task<IActionResult> Index()
        {
           
            return View(await _context.Abouts.ToListAsync());
        }
        //About index Function end

    }
}
