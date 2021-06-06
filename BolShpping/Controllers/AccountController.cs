using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BolShpping.Models.BLL;
using BolShpping.Models.DAL;
using BolShpping.Models.VM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace BolShpping.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;

        public AccountController(MyContext context,
                              UserManager<AppUser> userManager,
                              SignInManager<AppUser> signInManager,
                              RoleManager<IdentityRole> roleManager,
                              IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }
        public IActionResult Register()
        {
            return View();
        }
        //User Post Register Page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerModel);
            }
            AppUser user = registerModel;
            user.Name = registerModel.Name;

            IdentityResult result = await _userManager.CreateAsync(user, registerModel.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(registerModel);

            }

            // email confirmation
            //SMTP - Sinple Mail Transfer  Protocol

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            //64 byte array conversion part
            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
            var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);

            await _emailSender.SendEmailAsync(registerModel.Email, "Confirm your email",

                 $"Confirm your account by following to " +
                $"<a href='{HtmlEncoder.Default.Encode($"https://localhost:44303/Account/ConfirmEmail?token={codeEncoded}&userId={user.Id}")}'>" +
                "this link" +
                $"</a>");


            return View("VerifyEmail");
        }
   
        //Sending mail to user section Start
        public async Task<IActionResult> ConfirmEmail (string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var codeDecodedBytes = WebEncoders.Base64UrlDecode(token);
                var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);

                IdentityResult result = await _userManager.ConfirmEmailAsync(user, codeDecoded);

                if (result.Succeeded)
                {
                    return View();
                }

            }
            return View("FailedConfirmation");
        }

        //User signin Section Start
        public IActionResult Signin()
        {
            return View();
        }

        //User Post signin Section 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signin(UserLoginModel userLoginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userLoginModel);
            }

            AppUser user = await _userManager.FindByEmailAsync(userLoginModel.EmailOrUsernaem);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(userLoginModel.EmailOrUsernaem);
            }

            if (user == null)
            {
                ModelState.AddModelError("", "Email or Password is invalid");
                return View(userLoginModel);
            }

            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError("", "Your Email is not confirmet yet. Please, check your email");
                return View(userLoginModel);
            }

            var result = await _signInManager.PasswordSignInAsync(user, userLoginModel.Password, userLoginModel.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Email or Password is invalid");
            return View(userLoginModel);
        }
        //User Signout Section Start
        public IActionResult Signout()
        {
            _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
