using Collage_Moodle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Collage_Moodle.Controllers
{
    public class PermissionController : Controller
    {
        // GET: Permission
        public ActionResult CheckPermission(Users user)
        {
            if (user == null)
                return RedirectToAction("Login", "Login");

            else if (user.permission == 0)
                return RedirectToAction("Index", "Student");

            else if (user.permission == 1)
                return RedirectToAction("Index", "Lecturer");

            else
                return RedirectToAction("Index", "FacultyAdministrator");
        }
    }
}