using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorIdentity.DALL.Models;
using MentorIdentity.DALL.ViewModels;
using MentorIdntity.BLL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MentorIdentity.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AccountService _accountService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, AccountService service)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = service;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =  await _accountService.Register(model);
                if (result.Identity.Succeeded)
                {
                    await _signInManager.SignInAsync(result.User, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Identity.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
    }
}
