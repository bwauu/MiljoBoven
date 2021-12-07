using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiljoBoven.Models;

namespace MiljoBoven.Controllers
{
    public class InvestigatorController : Controller
    {
        private IEnvironmentCrimeRepository repository;
        
        public InvestigatorController(IEnvironmentCrimeRepository repo)
        {
            repository = repo;
        }

        public ViewResult StartInvestigator()
        {
            ViewBag.Title = "Start - Handläggare";
            return View(repository);
        }
        
        public ViewResult CrimeInvestigator(string id)
        {
            ViewBag.ID = id;
            return View(repository);
        }
    }
}