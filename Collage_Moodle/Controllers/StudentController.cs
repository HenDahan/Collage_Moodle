using Collage_Moodle.Dal;
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
        DAL dal = new DAL();
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

        public ActionResult ViewSchedule()
        {
            Users thisUser = (Users)Session["user"];
            if (thisUser == null)
                return RedirectToAction("Index", "Login");
            else if (thisUser.permission != 0)
                return perm.CheckPermission(thisUser);
            else
            {
                List<Courses> dbcourses = (from course in dal.Courses
                                           join student in dal.Students on course.courseName equals student.Courses_cName
                                           where student.Users_userID.Equals(thisUser.userID)
                                           select course).ToList<Courses>();
                if (dbcourses.Count > 0)
                {
                    List<CourseModel> showCourses = new List<CourseModel>();
                    foreach (Courses c in dbcourses)
                        showCourses.Add(new CourseModel { courseName = c.courseName, day = c.day, hour = c.hour, classroom = c.classroom, lecturerID = c.Users_lecturerID });

                    ViewSchedule studentView = new ViewSchedule();
                    studentView.user = thisUser;
                    studentView.courses = showCourses;
                    return View(studentView);

                }
                else
                {
                    TempData["Message"] = "You don't have any schedule yet.";
                    return perm.CheckPermission(thisUser);
                }
            }
               
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