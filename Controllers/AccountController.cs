using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Data;
using Test.Models;
using Test.Models.Helper;

namespace Test.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(ApplicationDbContext db, UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager )
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public ActionResult Login()
        { 
             return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(Login model)
        {

            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");

                }
                ModelState.AddModelError("", "Username or password incorrect");
            }
            return View(model);
        }
        public IActionResult Register()
        {
            if(!_roleManager.RoleExistsAsync(Helper.Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(Helper.Admin));
                _roleManager.CreateAsync(new IdentityRole(Helper.User));
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Register model)
        {
           if(ModelState.IsValid)
            {
                var usr = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(usr,model.Password);
                if(result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(usr, model.RoleName);
                    await _signInManager.SignInAsync(usr, false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

    }
}
