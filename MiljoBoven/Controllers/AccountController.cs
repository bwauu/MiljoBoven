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

        // UserManager & SignInManager objects declared!
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        // Public AccountController constructor. 
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [AllowAnonymous] // alla har tillgång till loggin-sidan.
        public ViewResult Login(string returnURL)
        {
            return View(new LoginModel { ReturnURL = returnURL});
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken] // Skyddar mot XXS, taghelper
        public async Task<IActionResult> Login (LoginModel loginModel)
        {
            IdentityUser user = await userManager.FindByNameAsync(loginModel.UserName); // Identity "user" object is refering to the LoginModel's UserName type string.

            if (ModelState.IsValid)
            {
                if (user != null) // kontrollera att namnet finns i db
                {
                    await signInManager.SignOutAsync(); // Om användaren är redan inlogga så tas sessionen bort
                    if ((await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {
                       if(await userManager.IsInRoleAsync(user, "Manager"))
                        {
                            return Redirect("/Manager/StartManager");
                        }

                        if (await userManager.IsInRoleAsync(user, "Coordinator"))
                        {
                            return Redirect("/Coordinator/StartCoordinator");
                        }

                        if (await userManager.IsInRoleAsync(user, "Investigator"))
                        {
                            return Redirect("/Investigator/StartInvestigator");
                        }
                    }
                }
            }
            ModelState.AddModelError("", "Felaktigt användarnamn eller lösenord!");
            return View(loginModel);
        }

        public async Task<RedirectResult> Logout(string returnURL = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnURL);
        }

        [AllowAnonymous]
        public ViewResult AccessDenied()
        {
            return View();
        }
    }
}
