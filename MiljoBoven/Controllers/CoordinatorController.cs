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
            if (newErrand == null)
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
            var errandDetail = repository.GetErrandDetail(id);
            ViewBag.Title = "Brottskoordinator - Samordnare";
            TempData["ID"] = id; // Via event i vyn anropas en metod
            ViewBag.ListOfDepartments = repository.Departments; // ViewBag i vyn crimecoordinator
            ViewBag.ID = id;

            return View(); ;
        }

        public async Task<IActionResult> SaveDepartment(string DepartmentId) // Hjälpmetod asp-for"
        {   // DepartmentId == SomeNewValue
            // 
            int someIDValue = int.Parse(TempData["ID"].ToString()); // Refererar till ett ID av någotslag.

            if (DepartmentId != null && DepartmentId != "Välj" && DepartmentId != "D00")
            {
                repository.UpdateDepartment(someIDValue, DepartmentId);
            }
            return RedirectToAction("CrimeCoordinator", new { id = someIDValue });


        }


    }
}