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

        public async Task<IActionResult> SaveEmployee(bool noAction, string EmployeeId, string InvestigatorInfo)
        {
            int someNewValue = int.Parse(TempData["ID"].ToString());


            if (noAction == false) // Om checkboxen är checkad =>
            {
                if (EmployeeId != null && EmployeeId != "Välj")
                {   
                    
                    repository.UpdateEmployee(someNewValue, EmployeeId);
                    repository.UpdateStatus(someNewValue, "S_A");
                }
            }
            if (noAction == true) // Om checkbox är inte checkad
            {   
                EmployeeId = "";
                repository.UpdateStatus(someNewValue, "S_B");
                repository.UpdateEmployee(someNewValue, EmployeeId);
                repository.UpdateInfo(someNewValue, InvestigatorInfo);
            }

            return RedirectToAction("CrimeManager", new { id = someNewValue });

        }

    }
}