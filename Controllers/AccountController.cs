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
                    return RedirectToAction("Visible", "Item");

                }
                ModelState.AddModelError("", "Username or password incorrect");
            }
            return View(model);
            var usrid = _userManager.GetUserId(HttpContext.User);
        }
        public IActionResult Register()
        {
            //checksifadminnexixtsornot
            if(!_roleManager.RoleExistsAsync(Helper.Admin).GetAwaiter().GetResult())
            {
                //createnewidentityroleifthydontexist
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
                    //two required properties
                    UserName = model.Email,
                    Email = model.Email,
                    //FirstName = model.FirstName,
                    //LastName = model.LastName,
                };
                var isEmailIdExists = _db.Users.Any(x => x.UserName == usr.Email);
                if(isEmailIdExists)
                {
                    ModelState.AddModelError("Email", "User with this Mail Already Exists");
                    return View(model);
                }
                //helper methods in user manager
                var result = await _userManager.CreateAsync(usr,model.Password);
                if(result.Succeeded)
                {
                    
                    await _userManager.AddToRoleAsync(usr, model.RoleName);
                    //automacatiallysigninthenewuser
                    await _signInManager.SignInAsync(usr, false);
                    return RedirectToAction("Login", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        

    }
}
