using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.WebPages.Html;
using BolShpping.Models.BLL;
using BolShpping.Models.DAL;
using BolShpping.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BolShpping.Controllers
{
    public class ContactController : Controller
    {
        private readonly MyContext _context;

        public ContactController(MyContext context)
        {
            _context = context;
        }
        //Contact index Function Start
        public IActionResult Index()
        {
            ViewModel vm = new ViewModel
            {
                Contacts = _context.Contacts.ToList(),
               
            };
            return View(vm);
        }
        //Post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _context.SendMessages.AddAsync(model.SendMessage);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        //Contact index Function End

    }


}
