using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiljoBoven.Models;
using MiljoBoven.Infrastructure;

namespace MiljoBoven.Controllers
{
    public class CoordinatorController : Controller
    {
        private IEnvironmentCrimeRepository repository;

        public CoordinatorController(IEnvironmentCrimeRepository repo)
        {
            repository = repo;
        }
        public ViewResult Validate()
        {
            ViewBag.Title = "Bekräfta - Samordnare";
            return View();
        }
        
        [HttpPost] // Script på serversidan
        public ViewResult Validate(Errand errand)
        {

            HttpContext.Session.SetJson("NewErrand", errand);
            return View(errand);
        }
        public ViewResult Thanks()
        {
            ViewBag.Title = "Tack - Samordnare";
            var errand = HttpContext.Session.GetJson<Errand>("NewErrand");
            // Metod för att spara
            HttpContext.Session.Remove("NewErrand");
            return View(errand);
        }

        public ViewResult StartCoordinator()
        {
            ViewBag.Title = "Start - Samordnare";
            return View(repository);
        }

        public ViewResult ReportCrime() // Formulär
        {
            ViewBag.Title = "Rapportera brott - Samordnare ";
            var newErrand = HttpContext.Session.GetJson<Errand>("NewErrand");
            if(newErrand == null)
            {
                return View();
            }
            else
            {
                return View(newErrand);
            }
        }

        public ViewResult CrimeCoordinator(int id)
        {
            ViewBag.Title = "Brottskoordinator - Samordnare";
            ViewBag.ID = id;
            return View(repository);
        }


    }
}