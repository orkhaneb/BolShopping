using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BolShpping.Controllers
{
    public class LoginregisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
