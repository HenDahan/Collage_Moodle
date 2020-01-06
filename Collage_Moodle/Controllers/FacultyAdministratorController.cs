using Collage_Moodle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Collage_Moodle.Controllers
{
    public class FacultyAdministratorController : Controller
    {
        private PermissionController perm = new PermissionController();
        // GET: FacultyAdministrator
        public ActionResult Index()
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 2)
                return perm.CheckPermission(user);
            else
                return View("FacultyAdministratorMainPage");
        }

        public ActionResult Assign_students()
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 2)
                return perm.CheckPermission(user);
            else
                return View("Assign_students");
        }

        public ActionResult Manage_course_schedule()
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 2)
                return perm.CheckPermission(user);
            else
                return View("Manage_course_schedule");
        }

        public ActionResult Manage_exam_schedule()
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 2)
                return perm.CheckPermission(user);
            else
                return View("Manage_exam_schedule");
        }

        public ActionResult Update_course_grades()
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 2)
                return perm.CheckPermission(user);
            else
                return View("Update_course_grades");
        }

        public ActionResult Exit()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

    }
}