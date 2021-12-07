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
        
        public ViewResult CrimeManager(string id)
        {   
            ViewBag.Title = "Brott - Avdelningschef";
            ViewBag.ID = id;
            return View(repository);
        }
    }
}