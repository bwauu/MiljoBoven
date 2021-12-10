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
            ViewBag.Title = "Bekräfta - Samordnare";
            HttpContext.Session.SetJson("NewErrand", errand);
            return View(errand);
        }
        public ViewResult Thanks()
        {
            ViewBag.Title = "Tack - Samordnare";
            var errand = HttpContext.Session.GetJson<Errand>("NewErrand");
            // Metod för att spara
            repository.SaveErrand(errand);
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
            TempData["ID"] = id;
            ViewBag.ListOfDepartments = repository.Departments;
            ViewBag.ID = id;

            return View(); ;

        }

        public async Task<IActionResult> RefreshDepartment(string DepartmentId)
        {
            int someID = int.Parse(TempData["ID"].ToString());


            if (DepartmentId != null || DepartmentId != "Välj" || DepartmentId != "Småstads kommun")
            {
                repository.UpdateDepartment(someID, DepartmentId);
            }
            return RedirectToAction("CrimeCoordinator", new { id = someID });
        }

       
    }
}