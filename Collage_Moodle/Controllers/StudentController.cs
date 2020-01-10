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
                List<Courses> dbCourses = (from course in dal.Courses
                                           join student in dal.Students on course.courseName equals student.Courses_cName
                                           where student.Users_userID.Equals(thisUser.userID)
                                           select course).ToList<Courses>();
                if (dbCourses.Count > 0)
                {
                    List<CourseModel> showCourses = new List<CourseModel>();
                    foreach (Courses c in dbCourses)
                        showCourses.Add(new CourseModel { courseName = c.courseName, day = c.day, hour = c.hour, classroom = c.classroom, lecturerID = c.Users_lecturerID });

                    ViewSchedule studentView = new ViewSchedule();
                    studentView.user = thisUser;
                    studentView.courses = showCourses;
                    return View(studentView);

                }
                else
                {
                    TempData["Message"] = "You do not have any schedule yet.";
                    return perm.CheckPermission(thisUser);
                }
            }
               
        }

        public ActionResult ViewExamSchedule()
        {
            Users thisUser = (Users)Session["user"];
            if (thisUser == null)
                return RedirectToAction("Index", "Login");
            else if (thisUser.permission != 0)
                return perm.CheckPermission(thisUser);
            else
            {
                List<Exams> dbExams = (from exam in dal.Exams
                                       join student in dal.Students on exam.Courses_cName equals student.Courses_cName
                                           where student.Users_userID.Equals(thisUser.userID)
                                           select exam).ToList<Exams>();
                if (dbExams.Count > 0)
                {
                    List<ExamModel> showExams = new List<ExamModel>();
                    foreach (Exams e in dbExams)
                        showExams.Add(new ExamModel { Courses_cName = e.Courses_cName, moed = e.moed, date = e.date, hour = e.hour, classroom = e.classroom });


                    ViewExamSchedule examView = new ViewExamSchedule();
                    examView.user = thisUser;
                    examView.exams = showExams;
                    return View(examView);

                }
                else
                {
                    TempData["Message"] = "You do not have any exam schedule yet.";
                    return perm.CheckPermission(thisUser);
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