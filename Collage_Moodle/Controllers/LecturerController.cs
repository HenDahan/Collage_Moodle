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

        public ActionResult View_schedule()
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 1)
                return perm.CheckPermission(user);
            else
                return View("View_schedule");
        }

        public ActionResult view_student_list_of_a_course()
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 1)
                return perm.CheckPermission(user);
            else
                return View("view_student_list_of_a_course");
        }

        public ActionResult View_exam_grades()
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 1)
                return perm.CheckPermission(user);
            else
                return View("View_exam_grades");
        }

        public ActionResult Update_course_grades()
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 1)
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