using Microsoft.AspNetCore.Mvc;
using MiljoBoven.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiljoBoven.Components
{
    public class StartErrandViewComponent : ViewComponent
    {
        private IEnvironmentCrimeRepository repository;
        public StartErrandViewComponent(IEnvironmentCrimeRepository repository)
        {
            this.repository = repository;
        }
        public IViewComponentResult Invoke()
        {
            return View("StartErrand");
        }
    }
}
