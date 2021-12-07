using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiljoBoven.Models;

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
            return View(errand);
        }
        public ViewResult Thanks()
        {
            ViewBag.Title = "Tack - Samordnare";
            return View();
        }

        public ViewResult StartCoordinator()
        {
            ViewBag.Title = "Start - Samordnare";
            return View(repository);
        }

        public ViewResult ReportCrime()
        {
            ViewBag.Title = "Rapportera brott - Samordnare ";
            return View();
        }

        public ViewResult CrimeCoordinator(string id)
        {
            ViewBag.Title = "Brottskoordinator - Samordnare";
            ViewBag.ID = id;
            return View(repository);
        }


    }
}