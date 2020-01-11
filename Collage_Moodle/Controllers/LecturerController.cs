using Collage_Moodle.Dal;
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
        DAL dal = new DAL();

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
            {
                List<Courses> dbCourses = (from course in dal.Courses
                                           where course.Users_lecturerID.Equals(user.userID)
                                           select course).ToList<Courses>();
                if (dbCourses.Count > 0)
                {
                    List<CourseModel> showCourses = new List<CourseModel>();
                    foreach (Courses c in dbCourses)
                        showCourses.Add(new CourseModel { courseName = c.courseName, day = c.day, hour = c.hour, classroom = c.classroom });

                    ViewSchedule lecturerView = new ViewSchedule();
                    lecturerView.user = user;
                    lecturerView.courses = showCourses;
                    return View(lecturerView);
                }
                else
                {
                    TempData["Message"] = "You do not have any schedule yet.";
                    return perm.CheckPermission(user);
                }
            }
        }

        //checking list of students.
        public ActionResult ViewStudentList()
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 1)
                return perm.CheckPermission(user);
            else
            {
                ViewStudentList courseN = new ViewStudentList();
                return View(courseN);
            }
        }

        [HttpPost]
        public ActionResult ViewStudentList(ViewStudentList courseN)
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 1)
                return perm.CheckPermission(user);
            else
            {
                if (if_his_course(user.userID, courseN.course_name))
                {
                    List<Students> dbStudents = (from student in dal.Students
                                                 where student.Courses_cName.Equals(courseN.course_name)
                                                 select student).ToList<Students>();
                    if (dbStudents.Count > 0)
                    {
                        List<StudentModel> showStudents = new List<StudentModel>();
                        foreach (Students s in dbStudents)
                            showStudents.Add(new StudentModel { Users_userID = s.Users_userID });

                        ViewStudentList studentsView = new ViewStudentList();
                        studentsView.user = user;
                        studentsView.students = showStudents;
                        TempData["post"] = "1";
                        TempData["course"] = courseN.course_name;
                        return View(studentsView);
                    }
                    else
                    {
                        TempData["Message"] = "There are no students studying this course.";
                        return perm.CheckPermission(user);
                    }
                }
                else
                {
                    TempData["Message"] = "This is not one of your courses.";
                    return View();
                }
            }
        }


        //checking exam grades.
        public ActionResult ViewExamGrades()
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 1)
                return perm.CheckPermission(user);
            else
            {
                ViewExamGrades courseN = new ViewExamGrades();
                return View(courseN);
            }
        }

        [HttpPost]
        public ActionResult ViewExamGrades(ViewExamGrades courseN)
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 1)
                return perm.CheckPermission(user);
            else
            {
                if (if_his_course(user.userID, courseN.course_name))
                { 
                    List<Students> dbStudents = (from student in dal.Students
                                                 where student.Courses_cName.Equals(courseN.course_name)
                                                 select student).ToList<Students>();
                    if (dbStudents.Count > 0)
                    {
                        List<Exams> dbExam = (from x in dal.Exams
                                              where (x.Courses_cName.Equals(courseN.course_name))
                                              select x).ToList<Exams>();
                        //I need to check for moed A and moed B
                        //checkDates(DateTime.Now, dbExam[0].date, dbExam[0].hour)
                        //take it from down below.

                        //changes that moed A will always be first.
                        if (dbExam[0].moed.Equals('B'))
                        {
                            dbExam.Add(dbExam[0]);
                            dbExam.RemoveAt(0);
                        }
                        //if Moed B date passed.
                        if (checkDates(DateTime.Now, dbExam[1].date, dbExam[1].hour))
                        {
                            List<StudentModel> showStudents = new List<StudentModel>();
                            foreach (Students s in dbStudents)
                                showStudents.Add(new StudentModel { Users_userID = s.Users_userID, grade = s.grade });

                            ViewExamGrades gradesView = new ViewExamGrades();
                            gradesView.user = user;
                            gradesView.students = showStudents;
                            TempData["post"] = "1";
                            TempData["course"] = courseN.course_name;
                            TempData["moed"] = "B";
                            return View(gradesView);
                        }
                        //if moed B didn't pass but moed A passed.
                        else if (checkDates(DateTime.Now, dbExam[0].date, dbExam[0].hour))
                        {
                            List<StudentModel> showStudents = new List<StudentModel>();
                            foreach (Students s in dbStudents)
                                showStudents.Add(new StudentModel { Users_userID = s.Users_userID, grade = s.grade });

                            ViewExamGrades gradesView = new ViewExamGrades();
                            gradesView.user = user;
                            gradesView.students = showStudents;
                            TempData["post"] = "1";
                            TempData["course"] = courseN.course_name;
                            TempData["moed"] = "A";
                            return View(gradesView);

                        }
                        //if none of the moeds started yet.
                        else
                        {
                            TempData["Message"] = "You cannot view the exams because the exam date did not passed yet.";
                            return View();
                        }
                    }
                    else
                    {
                        TempData["Message"] = "There are no students studying this course.";
                        return perm.CheckPermission(user);
                    }
                }
                else
                {
                    TempData["Message"] = "This is not one of your courses.";
                    return View();
                }

            }
        }

        public ActionResult UpdateCourseGrades()
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 1)
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
            else if (user.permission != 1)
                return perm.CheckPermission(user);
            else
            {
                string course_name = update_grade.course_name;
                int student_id = update_grade.student_ID;
                int new_grade = update_grade.grade;
                List<Exams> dbExam = (from x in dal.Exams
                                      where (x.Courses_cName.Equals(course_name))
                                      select x).ToList<Exams>();
                if (if_his_course(user.userID, course_name))
                {
                    //changes that moed A will always be first.
                    if (dbExam[0].moed.Equals('B'))
                    {
                        dbExam.Add(dbExam[0]);
                        dbExam.RemoveAt(0);
                    }

                    //if moed B date passed.
                    if (checkDates(DateTime.Now, dbExam[1].date, dbExam[1].hour))
                    {
                        List<Students> dbStudent = (from x in dal.Students
                                                    where (x.Users_userID.Equals(student_id) && x.Courses_cName.Equals(course_name))
                                                    select x).ToList<Students>();
                        if (dbStudent.Count > 0)
                        {
                            Students tempStudent = dal.Students.Single<Students>(x => x.Courses_cName == course_name && x.Users_userID == student_id);
                            tempStudent.grade = new_grade;
                            dal.SaveChanges();
                            TempData["Message"] = "Moed B grade Updated successfully";
                            return perm.CheckPermission(user);
                        }
                        else
                        {
                            TempData["Message"] = "There is no such user ID studing that course.";
                            return View();
                        }
                    }
                    //if moed B didn't pass but moed A passed.
                    else if (checkDates(DateTime.Now, dbExam[0].date, dbExam[0].hour))
                    {
                        List<Students> dbStudent = (from x in dal.Students
                                                    where (x.Users_userID.Equals(student_id) && x.Courses_cName.Equals(course_name))
                                                    select x).ToList<Students>();
                        if (dbStudent.Count > 0)
                        {
                            Students tempStudent = dal.Students.Single<Students>(x => x.Courses_cName == course_name && x.Users_userID == student_id);
                            tempStudent.grade = new_grade;
                            dal.SaveChanges();
                            TempData["Message"] = "Moed A grade Updated successfully";
                            return perm.CheckPermission(user);
                        }
                        else
                        {
                            TempData["Message"] = "There is no such user ID studing that course.";
                            return View();
                        }
                    }
                    //if none of the moeds started yet.
                    else
                    {
                        TempData["Message"] = "You cannot update a grade for this exam yet.(wait for the moed date to pass)";
                        return View();
                    }

                }
                else
                {
                    TempData["Message"] = "There is not one of your courses.";
                    return View();
                }
            }

        }
        public bool checkDates(DateTime now, string date, string hour)
        {
            string newcheck = date + " " + hour;
            newcheck = newcheck.Substring(0, 16);
            DateTime check = DateTime.Parse(newcheck);
            if (now >= check)
                return true;
            return false;
        }


        private bool if_his_course(int lecturerID, string courseName)
        {
            List<Courses> dbcourse = (from x in dal.Courses
                                      where (x.courseName.Equals(courseName) && x.Users_lecturerID.Equals(lecturerID))
                                        select x).ToList<Courses>();
            if (dbcourse.Count > 0)
                return true;
            return false;
        }

        public ActionResult Exit()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}