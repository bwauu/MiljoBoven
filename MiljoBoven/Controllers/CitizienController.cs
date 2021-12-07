using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiljoBoven.Models;

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
            return View(errand);
        }
        public ViewResult Thanks()
        {
            ViewBag.Title = "Tack - Medlem";
            return View();
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