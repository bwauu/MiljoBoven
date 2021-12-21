using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
            ViewBag.Title = "Manager";
            return View(repository);
        }

        public ViewResult CrimeManager(int id)
        {
            ViewBag.Title = "Manager";
            ViewBag.ID = id;
            ViewBag.ListOfEmployees = repository.GetManagerEmployeeList();
            TempData["ID"] = id;

            return View();
        }

        public async Task<IActionResult> SaveManagerActions(bool noAction, string EmployeeId, string InvestigatorInfo)
        {
            // EmployeeId == SomeNewValue
            int someIDValue = int.Parse(TempData["ID"].ToString());


            if (noAction == false) // Om checkboxen �r checkad => 
            {
                if (EmployeeId != null && EmployeeId != "V�lj")
                {   
                    
                    repository.UpdateEmployee(someIDValue, EmployeeId);
                    repository.UpdateStatus(someIDValue, "S_A");
                }
            }
            if (noAction == true) // Om checkbox �r inte checkad
            {   
          
                repository.UpdateStatus(someIDValue, "S_B");
                repository.UpdateInfo(someIDValue, InvestigatorInfo);
            }

            return RedirectToAction("CrimeManager", new { id = someIDValue });
        }
    }
}