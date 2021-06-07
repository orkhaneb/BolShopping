using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolShpping.Models.BLL;
using BolShpping.Models.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BolShpping.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    [Route("WebCms/[controller]/[action]")]
    public class ContactController : Controller
    {
        public readonly MyContext _context;

        public ContactController(MyContext context)
        {
            _context = context;
        }
        //Contact index Function Start
        public async Task<IActionResult> Index()
        {
            return View(await _context.Contacts.ToListAsync());
        }
        //Contact index Function End

        //Contact Create Function Start
        public IActionResult Create()
        {
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Contact contact)
        {
            if (ModelState.IsValid)
            {
                var result = await _context.Contacts.AddAsync(contact);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        //Contact Create Function End

        //Contact Edit Function Start
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var info = await _context.Contacts.FindAsync(id);

            if (info == null)
            {
                return NotFound();
            }
            return View(info);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( int id, Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(contact);
            }
          
            _context.Update(contact);
            await _context.SaveChangesAsync();

            var ProContact = await _context.Contacts.FindAsync(id);

            ProContact.Title = contact.Title;
            ProContact.Description = contact.Description;
            ProContact.Addres = contact.Addres;
            ProContact.Email = contact.Email;
            ProContact.Number = contact.Number;
            ProContact.WorkingHourse = contact.WorkingHourse;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        //Contact Edit Function End

        //Contact Delete Function Start
        public async Task<IActionResult> Delete(int id)
        {
            var ProContact = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(ProContact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //Contact Delete Function End

    }
}
