using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MiljoBoven.Infrastructure;
using MiljoBoven.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MiljoBoven.Controllers
{
    public class InvestigatorController : Controller
    {

        private IEnvironmentCrimeRepository repository;
        private IWebHostEnvironment environment;


        public InvestigatorController(IEnvironmentCrimeRepository repo, IWebHostEnvironment env)
        {
            repository = repo;
            environment = env;

        }

        public ViewResult CrimeInvestigator(int id)
        {
            ViewBag.ListOfStatuses = repository.ErrandStatuses;
            ViewBag.Title = "Investigator";
            ViewBag.ID = id;
            TempData["ID"] = id;

            return View();

        }


        public ViewResult StartInvestigator()
        {
            ViewBag.Title = "Investigator";
            return View(repository);
        }

        public async Task<IActionResult> UploadFiles(IFormFile loadSample, IFormFile loadImage, string events, string information, string StatusId)
        {
            int someID = int.Parse(TempData["ID"].ToString());


            // OM StatusId inte har något värde OCH OM StatusId värde INTE är LIKA MED strängen "Välj" 
            // OCH OM StatusId INTE är LIKA MED strängen "S_A" OCH OM StatusId INTE är LIKA MED strängen "S_B".
            if (StatusId != null && StatusId != "Välj" && StatusId != "S_A" && StatusId != "S_B")
            {
                repository.UpdateStatus(someID, StatusId);
            }

            if (events != null)
            {
                repository.UpdateAction(someID, events);
            }

            if (information != null)
            {
                repository.UpdateInfo(someID, information);
            }

            // Den temporära sökvägen
            var tempPath = Path.GetTempFileName();

            /* ********************************************************************************* FÖR UPPLADNING AV BILDER *************************************************************************** */
            if (loadImage != null)
            {
                // Om det är sant så finns det innehåll.
                if (loadImage.Length > 0)
                {
                    // Efter ovanstående kod exekveras så vill öppna en ström för denna fil.
                    using (var stream = new FileStream(tempPath, FileMode.Create))
                    {
                        await loadImage.CopyToAsync(stream); // Kopplar till strömmen.
                    }
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + loadImage.FileName;  // Guid.NewGuid metoden skapar ett nytt globalt user id 

                // Skapa ny sökväg
                var path = Path.Combine(environment.WebRootPath, "pictures", uniqueFileName); // Mappen måste var skapad innan. wwwroot får ladda ner data.

                //flytta den temporära filen rätt
                System.IO.File.Move(tempPath, path);

                ViewBag.FileName = uniqueFileName;

                ViewBag.Path = path;

                var newPicture = new Picture { ErrandId = someID, PictureName = uniqueFileName };
                repository.UpdatePictures(newPicture);
            }


            /* ********************************************************************************* FÖR UPPLADNING AV PROVER *************************************************************************** */
            if (loadSample != null)
            {
                // Om det är sant så finns det innehåll.
                if (loadSample.Length > 0)
                {
                    // Efter ovanstående kod exekveras så vill öppna en ström för denna fil.
                    using (var stream = new FileStream(tempPath, FileMode.Create))
                    {
                        await loadSample.CopyToAsync(stream); // Kopplar till strömmen.
                    }
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + loadSample.FileName;  // Guid.NewGuid metoden skapar ett nytt globalt user id 

                // Skapa ny sökväg
                var path = Path.Combine(environment.WebRootPath, "samples", uniqueFileName); // Mappen måste var skapad innan. wwwroot får ladda ner data.

                //flytta den temporära filen rätt
                System.IO.File.Move(tempPath, path);

                ViewBag.FileName = uniqueFileName;

                ViewBag.Path = path;

                var newSample = new Sample { ErrandId = someID, SampleName = uniqueFileName };
                repository.UpdateSamples(newSample);
            }


            return RedirectToAction("CrimeInvestigator", new { id = someID });

        }

    }
}

