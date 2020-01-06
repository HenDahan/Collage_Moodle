using Collage_Moodle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Collage_Moodle.Controllers
{
    public class StudentController : Controller
    {
        private PermissionController perm = new PermissionController();
        // GET: Student
        public ActionResult Index()
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 0)
                return perm.CheckPermission(user);
            else
                return View("StudentMainPage");
        }

        public ActionResult View_schedule()
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 0)
                return perm.CheckPermission(user);
            else
                return View("View_schedule");
        }

        public ActionResult View_exam_schedule()
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 0)
                return perm.CheckPermission(user);
            else
                return View("View_exam_schedule");
        }


        public ActionResult Exit()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}