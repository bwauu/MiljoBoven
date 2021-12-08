using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiljoBoven.Models;
using MiljoBoven.Infrastructure;

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
        public ViewResult Login()
        {
            ViewBag.Title = "Login";
            return View();
        }
  

    }
}