using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiljoBoven.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiljoBoven.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManagerx, SignInManager<IdentityUser> signInManagerx)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [AllowAnonymous] // alla kan kommma åt sidan
        public ViewResult Login(string returnURL)
        {
            return View(new LoginModel { ReturnURL = returnURL});
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken] // Skyddar mot XXS, taghelper
        public async Task<IActionResult> Login (LoginModel loginModel)
        {   
            IdentityUser user = await userManager.FindByNameAsync(loginModel.UserName);
            
            if(ModelState.IsValid)
            {
                if (user != null) // kontrollera att namnet finns i db
                {
                    await signInManager.SignOutAsync(); // Om användaren är redan inlogga så tas sessionen bort
                    if ((await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {
                        return Redirect("/Account/Manager");
                    }
                }
            }
            ModelState.AddModelError("", "Felaktigt användarnamn eller lösenord");
            return View(loginModel);
        }

        public async Task<RedirectResult> Logout(string returnURL = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnURL);
        }
    }
}
