using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiljoBoven.Models;
using MiljoBoven.Infrastructure;

namespace MiljoBoven.Controllers
{
    public class CitizienController : Controller
    {

        public ViewResult Validate()
        {
            ViewBag.Title = "Bekräfta - Medlem";
            return View();
        }
        
        [HttpPost]
        public ViewResult Validate(Errand errand)
        {
            ViewBag.Title = "Bekräfta - Medlem";

            HttpContext.Session.SetJson("NewErrand", errand);
            return View("Validate", errand);
 
        }
        public ViewResult Thanks()
        {
            ViewBag.Title = "Tack - Medlem";
            var errand = HttpContext.Session.GetJson<Errand>("NewErrand");
            // Metod behövs för att spara errand
            HttpContext.Session.Remove("NewErrand");
            return View(errand);
        }

        public ViewResult Services()
        {
            ViewBag.Title = "Tjänster - Medlem";
            return View();
        }

        public ViewResult FAQ()
        {
            ViewBag.Title = "FAQ - Medlem";
            return View();
        }

        public ViewResult Contact()
        {
            ViewBag.Title = "Kontakt - Medlem";
            return View();
        }
    }
}