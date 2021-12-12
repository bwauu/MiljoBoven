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
            TempData["ID"] = id; // Via event i vyn anropas en metod
            ViewBag.ListOfEmployees = repository.Employees;
            ViewBag.ID = id;
            return View();
        }

        public async Task<IActionResult> SaveEmployee(bool noAction, string EmployeeId, string InvestigatorInfo)
        {
            // EmployeeId == SomeNewValue
            int someIDValue = int.Parse(TempData["ID"].ToString());


            if (noAction == false) // Om checkboxen är checkad => 
            {
                if (EmployeeId != null || EmployeeId != "Välj")
                {   
                    
                    repository.UpdateEmployee(someIDValue, EmployeeId);
                    repository.UpdateStatus(someIDValue, "S_A");
                }
            }
            if (noAction == true) // Om checkbox är inte checkad
            {   
                EmployeeId = "";
                repository.UpdateStatus(someIDValue, "S_B");
                repository.UpdateEmployee(someIDValue, EmployeeId);
                repository.UpdateInfo(someIDValue, InvestigatorInfo);
            }

            return RedirectToAction("CrimeManager", new { id = someIDValue });

        }

    }
}