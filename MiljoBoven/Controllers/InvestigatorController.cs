using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiljoBoven.Models;

namespace MiljoBoven.Controllers
{
    public class InvestigatorController : Controller
    {
        private IEnvironmentCrimeRepository repository;
        private IWebHostEnvironment environment;

        public InvestigatorController(IWebHostEnvironment env)
        {
            environment = env;
        }
        public InvestigatorController(IEnvironmentCrimeRepository repo)
        {
            repository = repo;
        }

        public ViewResult StartInvestigator()
        {
            ViewBag.Title = "Start - Handläggare";
            return View(repository);
        }
        
        public ViewResult CrimeInvestigator(int id)
        {
            ViewBag.ID = id;
            return View(repository);
        }

        public async Task<IActionResult> UploadFiles(IFormFile documents)
        {
            // Den temporära sökvägen
            var tempPath = Path.GetTempFileName();

            // Om det är sant så finns det innehåll.
            if (documents.Length > 0)
            {
                // Efter ovanstående kod exekveras så vill öppna en ström för denna fil.
                using (var stream = new FileStream(tempPath, FileMode.Create))
                {
                    await documents.CopyToAsync(stream); // Kopplar till strömmen.
                }
            }

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + documents.FileName;  // Guid.NewGuid metoden skapar ett nytt globalt user id 

            // Skapa ny sökväg
            var path = Path.Combine(environment.WebRootPath, "Uploads", uniqueFileName); // Mappen måste var skapad innan. wwwroot får ladda ner data.

            //flytta den temporära filen rätt
            System.IO.File.Move(tempPath, path);

            ViewBag.FileName = uniqueFileName;


            ViewBag.Path = path;
            return View("SavedFile");




        }
    }
}