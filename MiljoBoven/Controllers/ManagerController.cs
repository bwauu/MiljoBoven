using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiljoBoven.Models;

namespace MiljoBoven.Controllers
{
    public class ManagerController : Controller
    {
        private IEnvironmentCrimeRepository repository;

        public ManagerController(IEnvironmentCrimeRepository repo)
        {
            repository = repo;
        }

        public ViewResult StartManager()
        {
            ViewBag.Title = "Start - Avdelningschef";
            return View(repository);
        }

        public ViewResult CrimeManager(int id)
        {
            ViewBag.Title = "Brott - Avdelningschef";
            ViewBag.ListOfEmployees = repository.Employees;
            ViewBag.ID = id;
            return View();
        }

        public async Task<IActionResult> SaveEmployee(bool isActionChecked, string EmployeeId, string InvestigatorInfo)
        {
            int someID = int.Parse(TempData["ID"].ToString());


            if (isActionChecked != true)
            {
                if (EmployeeId != null || EmployeeId != "Välj")
                {
                    repository.UpdateEmployee(someID, EmployeeId);
                    repository.UpdateStatus(someID, "S_A");
                }
            }
            if (isActionChecked == true)
            {
                repository.UpdateStatus(someID, "S_B");
                repository.UpdateEmployee(someID, EmployeeId);
                repository.UpdateInfo(someID, InvestigatorInfo);
            }

            return RedirectToAction("CrimeManager", new { id = someID });

        }

    }
}