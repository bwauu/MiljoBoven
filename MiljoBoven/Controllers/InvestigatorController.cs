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
        /*
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


            */

        public async Task<IActionResult> Uploads(IFormFile loadSample, IFormFile loadImage, string events, string information, string StatusId)
        {
            int someID = int.Parse(TempData["ID"].ToString());



            if (StatusId != null && StatusId != "Välj")
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

            var tempPath = Path.GetTempFileName();

            if (loadSample != null || loadImage != null)
            {

                if (loadSample != null || loadSample.Length > 0)
                {
                    using (var stream = new FileStream(tempPath, FileMode.Create))
                    {
                        await loadSample.CopyToAsync(stream);
                    }
                    var samplePath = Path.Combine(environment.WebRootPath, "samples", loadSample.FileName);
                    System.IO.File.Move(tempPath, samplePath);

                    ViewBag.FileName = loadSample.FileName;
                    ViewBag.Path = samplePath;
                }

                var updateSample = new Sample { ErrandId = someID, SampleName = loadSample.FileName };

                repository.UpdateSamples(updateSample);

                if (loadImage != null || loadImage.Length > 0)
                {
                    using (var stream = new FileStream(tempPath, FileMode.Create))
                    {
                        await loadImage.CopyToAsync(stream);
                    }

                    var imagePath = Path.Combine(environment.WebRootPath, "pictures", loadImage.FileName);

                    System.IO.File.Move(tempPath, imagePath);

                    ViewBag.FileName = loadImage.FileName;
                    ViewBag.Path = imagePath;
                }

                var updatePicture = new Picture { ErrandId = someID, PictureName = loadImage.FileName };

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