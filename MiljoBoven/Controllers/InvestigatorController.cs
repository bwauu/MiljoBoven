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


        public async Task<IActionResult> UploadFiles(IFormFile loadSample, IFormFile loadImage, string events, string information, string StatusId)
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
                    var samplePath = Path.Combine(enviroment.WebRootPath, "samples", loadSample.FileName);
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

                    var imagePath = Path.Combine(enviroment.WebRootPath, "pictures", loadImage.FileName);

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

