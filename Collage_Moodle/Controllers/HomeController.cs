using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Collage_Moodle.Controllers
{
    public class HomeController : Controller
    {
        // GET /home/index
        public ActionResult Index()
        {
            return View();
        }


        // GET /home/about
        [ActionName("About-us")]
        public ActionResult About()
        {
            ViewBag.TheMessage = "like it ? let us know.";

            return View("About");
        }

        public ActionResult Contact()
        {
            ViewBag.TheMessage = "like it ? let us know.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(string message)
        {
            //TODO : send message to HQ
            ViewBag.TheMessage = "thanks, we got your message.";

            return View();
        }

        public ActionResult Serial(string letterCase)
        {

            var serial = "ASPMVC";
            if (letterCase == "lower")
            {
                return Content(serial.ToLower());
            }
            //return new HttpStatusCodeResult(403);
            //return Json(new { name = "kfir", value = serial }, JsonRequestBehavior.AllowGet );
            return RedirectToAction("Index");
        }
    }
}