using Collage_Moodle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Collage_Moodle.Controllers
{
    public class LecturerController : Controller
    {
        private PermissionController perm = new PermissionController();
        public ActionResult Index()
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 1)
                return perm.CheckPermission(user);
            else
                return View("LecturerMainPage");
        }

        public ActionResult ViewSchedule()
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 1)
                return perm.CheckPermission(user);
            else
                return View("ViewSchedule");
        }

        public ActionResult ViewStudentListOfaCourse()
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 1)
                return perm.CheckPermission(user);
            else
                return View("ViewStudentListOfaCourse");
        }

        public ActionResult ViewExamGrades()
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 1)
                return perm.CheckPermission(user);
            else
                return View("ViewExamGrades");
        }

        public ActionResult UpdateCourseGrades()
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 1)
                return perm.CheckPermission(user);
            else
                return View("UpdateCourseGrades");
        }

        public ActionResult Exit()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}