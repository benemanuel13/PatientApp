using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TGUWeb.Infrastructure;
using TGUWeb.Models.WebApiModels;

namespace TGUWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ServiceUser user = Database.GetServiceUser(1);

            return View(user);
        }

        public ActionResult CarePlan()
        {
            Models.WebApiModels.CarePlan plan = Database.GetCarePlan(1);

            return View(plan);
        }

        public FileResult Download()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"c:\inetpub\TGUApp\Care Plans\Ben Emanuel Care Plan.docx");
            string fileName = "Ben Emanuel Care Plan.docx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public FileResult ViewVideo()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"c:\inetpub\TGUApp\Videos\BenTest.mp4");
            string fileName = "BenTest.mp4";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        public ActionResult InterventionVideo()
        {
            return View();
        }

        public ActionResult PatientVideos()
        {
            List<PatientVideo> videos = Database.GetPatientVideos();

            return View(videos);
        }

        public ActionResult Installation()
        {
            return View();
        }

        //public ActionResult Challenge(Challenge challenge)
        //{
        //    return PartialView("_Challenge", challenge);
        //}

        public ActionResult Demo()
        {
            return View();
        }
    }
}