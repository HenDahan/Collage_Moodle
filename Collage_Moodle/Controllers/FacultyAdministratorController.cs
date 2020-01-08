using Collage_Moodle.Dal;
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
        DAL dal = new DAL();
        // GET: FacultyAdministrator
        public ActionResult Index()
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 2)
                return perm.CheckPermission(user);
            else
            {
                return View("FacultyAdministratorMainPage");
            }
        }

        public ActionResult AssignStudents()
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 2)
                return perm.CheckPermission(user);
            else
            {
                AssignStudent assignS = new AssignStudent();

                return View(assignS);
            }
        }


        [HttpPost]
        public ActionResult AssignStudents(AssignStudent assignS)
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 2)
                return perm.CheckPermission(user);
            else
            {

                int userID = assignS.studentID;
                string courseName = assignS.courseName.ToString();
                dal.Students.Add(new Students { Courses_cName = courseName, Users_userID = userID });
                try
                {
                    dal.SaveChanges();
                }
                catch
                {
                    TempData["Message"] = "Assigned succesfully";
                    return perm.CheckPermission(user);

                }
                TempData["Message"] = "Assigned succesfully";
                return perm.CheckPermission(user);

            }

        }

        /*
        public JsonResult ViewCourseInfo(string courseName)
        {

            return Json();
        }*/


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

        public ActionResult UpdateCourseGrades()
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 2)
                return perm.CheckPermission(user);
            else
            {
                UpdateCourseGrades update_grade = new UpdateCourseGrades();
                return View(update_grade);
            }
        }

        [HttpPost]
        public ActionResult UpdateCourseGrades(UpdateCourseGrades update_grade)
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 2)
                return perm.CheckPermission(user);
            else
            {

                int userID = update_grade.studentID;
                int new_grade = update_grade.grade;
                string courseName = update_grade.courseName.ToString();
                List<Students> list = (from x in dal.Students
                                        where (x.Users_userID.Equals(userID) && x.Courses_cName.Equals(courseName))
                                        select x).ToList<Students>();
                if (list.Count > 0)
                {
                    Students dbstudent = dal.Students.Single<Students>(x => x.Courses_cName == courseName && x.Users_userID == userID);
                    dbstudent.grade = new_grade;
                    dal.SaveChanges();
                    TempData["Message"] = "Grade Updated succesfully";
                    return perm.CheckPermission(user);
                }
                else
                {
                    TempData["Message"] = "There is no such user ID studing that course.";
                    return perm.CheckPermission(user);
                }
            }

        }

        public ActionResult Exit()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

    }
}