using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolShpping.Models.DAL;
using Microsoft.AspNetCore.Mvc;

namespace BolShpping.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    [Route("WebCms/[controller]/[action]")]
    public class SendMessageController : Controller
    {
        private readonly MyContext _context;

        public SendMessageController(MyContext context)
        {
            _context = context;
        }
        //SendMessage index Function Start

        public IActionResult Index()
        {
            return View(_context.SendMessages.ToList());
        }
        //SendMessage index Function End

        //SendMessage Delete Function Start
        public async Task<IActionResult> Delete(int id)
        {
            var message = await _context.SendMessages.FindAsync(id);
            _context.SendMessages.Remove(message);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //SendMessage Delete Function End

        //SendMessage MoreDetailed Function Start

        public async Task<IActionResult> MoreDetailed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var info = await _context.SendMessages.FindAsync(id);

            if (info == null)
            {
                return NotFound();
            }
            return View(info);
        }
        //SendMessage MoreDetailed Function End

    }
}
