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
        private IWebHostEnvironment enviroment;


        public InvestigatorController(IEnvironmentCrimeRepository repo, IWebHostEnvironment env)
        {
            repository = repo;
            enviroment = env;

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


        public async Task<IActionResult> UploadFiles(IFormFile loadSample, IFormFile loadPicture, string events, string information, string StatusId)
        {
            int someID = int.Parse(TempData["ID"].ToString());



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

            if (loadSample != null || loadPicture != null)
            {
                // Om det är sant så finns det innehåll.
                if (loadSample.Length > 0)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + loadSample.FileName;  // Guid.NewGuid metoden skapar ett nytt globalt user id 
                    // Efter ovanstående kod exekveras så vill öppna en ström för denna fil.
                    using (var stream = new FileStream(tempPath, FileMode.Create))
                    {
                        await loadSample.CopyToAsync(stream); // Kopplar till strömmen.
                    }
                    var samplePath = Path.Combine(enviroment.WebRootPath, "samples", uniqueFileName);
                    System.IO.File.Move(tempPath, samplePath);
                    
                    ViewBag.FileName = uniqueFileName;
                    ViewBag.Path = samplePath;
                }

                var updateSample = new Sample { ErrandId = someID, SampleName = loadSample.FileName };

                repository.UpdateSamples(updateSample);

                if (loadPicture.Length > 0)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + loadPicture.FileName;  // Guid.NewGuid metoden skapar ett nytt globalt user id 
                    using (var stream = new FileStream(tempPath, FileMode.Create))
                    {
                        await loadPicture.CopyToAsync(stream);
                    }

                    var picturePath = Path.Combine(enviroment.WebRootPath, "pictures", uniqueFileName);

                    System.IO.File.Move(tempPath, picturePath);

                    ViewBag.FileName = loadPicture.FileName;
                    ViewBag.Path = picturePath;
                }

                var updatePicture = new Picture { ErrandId = someID, PictureName = loadPicture.FileName };

                repository.UpdatePictures(updatePicture);

                return RedirectToAction("CrimeInvestigator", new { id = someID });

            }
            else
            {
                return RedirectToAction("CrimeInvestigator", new { id = someID });

            }

        }
    }
}

