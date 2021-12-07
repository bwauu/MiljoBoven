using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiljoBoven.Models;

namespace MiljoBoven.Controllers
{
    public class HomeController : Controller
    {
        private IEnvironmentCrimeRepository repository;

        public HomeController(IEnvironmentCrimeRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index()
        {
            ViewBag.Title = "Hem";
            return View();
        }
        public ViewResult Login()
        {
            ViewBag.Title = "Login";
            return View();
        }
  

    }
}