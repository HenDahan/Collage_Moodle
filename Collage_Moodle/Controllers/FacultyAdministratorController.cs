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
                string courseName = assignS.course_name.ToString();
                int studentID = assignS.student_ID;

                List<Courses> dbCourse = (from x in dal.Courses
                                           where (x.courseName.Equals(courseName))
                                            select x).ToList<Courses>();

                //checking if this course exist.
                if (dbCourse.Count > 0)
                {
                    List<Students> dbStudent = (from x in dal.Students
                                                where (x.Users_userID.Equals(studentID) && x.Courses_cName.Equals(courseName))
                                                select x).ToList<Students>();
                    //checking if this student is not already studying this course.
                    if (dbStudent.Count == 0)
                    {
                        //checking if this course doesn't collide with another course time.
                        if (check_student_hours(studentID, courseName))
                        {
                            dal.Students.Add(new Students { Courses_cName = courseName, Users_userID = studentID });
                            dal.SaveChanges();

                            TempData["Message"] = "Assigned successfully";
                            return perm.CheckPermission(user);
                        }
                        else
                        {
                            TempData["Message"] = "Failed - The student cannot study two courses at the same time!";
                            return View();
                        }
                    }
                    else
                    {
                        TempData["Message"] = "Assigned Failed, The student is in this course already exists.";
                        return View();
                    }
                }
                else
                {
                    TempData["Message"] = "The course you tried to assign, does not exist.";
                    return View();
                }
            }

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
                string course_name = update_grade.course_name;
                int student_id = update_grade.student_ID;
                int new_grade = update_grade.grade;
                List<Exams> dbExam = (from x in dal.Exams
                                      where (x.Courses_cName.Equals(course_name))
                                            select x).ToList<Exams>();
                if (dbExam.Count == 2)
                {
                    if (dbExam.Count > 0)
                    {
                        //changes that moed A will always be first.
                        if (dbExam[0].moed.Equals('B'))
                        {
                            dbExam.Add(dbExam[0]);
                            dbExam.RemoveAt(0);
                        }

                        //if moed B date passed already.
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
                        TempData["Message"] = "There is no exam for that course name.";
                        return View();
                    }
                }
                else
                {
                    TempData["Message"] = "There are no signed exams for this course yet.";
                    return View();
                }
            }

        }
        public ActionResult ManageExamSchedule()
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 2)
                return perm.CheckPermission(user);
            else
            {
                ManageExamSchedule update_exam = new ManageExamSchedule();
                return View(update_exam);
            }
        }
        [HttpPost]
        public ActionResult ManageExamSchedule(ManageExamSchedule update_exam)
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 2)
                return perm.CheckPermission(user);
            else
            {
                string courseName = update_exam.course_name;
                string moed = update_exam.moed;
                string new_date = update_exam.date;
                string new_hour = update_exam.hour;
                string new_classroom = update_exam.classroom;
                List<Exams> dbExam = (from x in dal.Exams
                                    where (x.Courses_cName.Equals(courseName) && x.moed.Equals(moed))
                                       select x).ToList<Exams>();
                if (dbExam.Count > 0)
                {
                    Exams tempExam = dal.Exams.Single<Exams>(x => x.Courses_cName == courseName && x.moed == moed);
                    tempExam.date = new_date;
                    tempExam.hour = new_hour;
                    tempExam.classroom = new_classroom;

                    dal.SaveChanges();
                    TempData["Message"] = "The exam date and classroom UPDATED successfully.";
                    return perm.CheckPermission(user);
                }
                else
                {
                    dal.Exams.Add(new Exams { Courses_cName = courseName, moed = moed, date = new_date, hour = new_hour, classroom = new_classroom });
                    dal.SaveChanges();
                    TempData["Message"] = "The exam datae and class CREATED successfully.";
                    return perm.CheckPermission(user);
                }
            }
        }

        public ActionResult ManageCourseSchedule()
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 2)
                return perm.CheckPermission(user);
            else
            {
                ManageCourseSchedule update_course = new ManageCourseSchedule();
                return View(update_course);
            }
        }
        [HttpPost]
        public ActionResult ManageCourseSchedule(ManageCourseSchedule update_course)
        {
            Users user = (Users)Session["user"];
            if (user == null)
                return RedirectToAction("Index", "Login");
            else if (user.permission != 2)
                return perm.CheckPermission(user);
            else
            {
                string courseName = update_course.course_name;
                string day = update_course.day;
                string hour = update_course.hour;
                string classroom = update_course.classroom;
                int lecturerID = update_course.Lecturer_id;

                List<Courses> dbCourse = (from x in dal.Courses
                                        where (x.courseName.Equals(courseName))
                                      select x).ToList<Courses>();
                if (dbCourse.Count > 0)
                {
                    if (check_lecturer(lecturerID, day, hour))
                    {
                        Courses tempCourse = dal.Courses.Single<Courses>(x => x.courseName == courseName);
                        tempCourse.day = day;
                        tempCourse.hour = hour;
                        tempCourse.classroom = classroom;
                        tempCourse.Users_lecturerID = lecturerID;

                        dal.SaveChanges();
                        TempData["Message"] = "The course data has been UPDATED successfully.";
                        return perm.CheckPermission(user);
                    }
                    else
                    {
                        TempData["Message"] = "Error - The lecturer cannot lecture another course at the same time!";
                        return View();
                    }
                }
                else
                {
                    if (check_lecturer(lecturerID, day, hour))
                    {
                        dal.Courses.Add(new Courses { courseName = courseName, day = day, hour = hour, classroom = classroom, Users_lecturerID = lecturerID });
                        dal.SaveChanges();
                        TempData["Message"] = "The course data has been CREATED successfully.";
                        return perm.CheckPermission(user);
                    }
                    else
                    {
                        TempData["Message"] = "Error - The lecturer cannot lecture another course at the same time!";
                        return View();
                    }
                }
            }
        }

        private bool check_lecturer(int id, string day, string hour)
        {

            List<Courses> dbCourses = (from x in dal.Courses
                                        where (x.Users_lecturerID.Equals(id))
                                        select x).ToList<Courses>();

            float[] l_hour = new float[2];
            float[] c_hour = new float[2];
            
            l_hour[0] = float.Parse(hour.Substring(0, 5).Replace(':', '.'));
            l_hour[1] = float.Parse(hour.Substring(6, 5).Replace(':', '.'));
            foreach (Courses course in dbCourses)
            {
                if (course.day == day)
                {
                    c_hour[0] = float.Parse(course.hour.Substring(0, 5).Replace(':', '.'));
                    c_hour[1] = float.Parse(course.hour.Substring(6, 5).Replace(':', '.'));

                    if ((l_hour[0] >= c_hour[0] && l_hour[0] < c_hour[1]) || (l_hour[1] > c_hour[0] && l_hour[1] <= c_hour[1]) || (c_hour[0] > l_hour[0] && c_hour[0] < l_hour[1]))
                    {
                        return false;
                    }
                }
            }
                return true;
        }

        private bool check_student_hours(int id, string courseName)
        {

            List<Courses> dbCourses = (from x in dal.Courses
                                       where (!x.courseName.Equals(courseName))
                                       select x).ToList<Courses>();
            List<Courses> hisCourse = (from x in dal.Courses
                                       where (x.courseName.Equals(courseName))
                                       select x).ToList<Courses>();
            
            float[] s_hour = new float[2];
            float[] c_hour = new float[2];
            string student_day = hisCourse[0].day;
            string student_hour = hisCourse[0].hour;
            s_hour[0] = float.Parse(student_hour.Substring(0, 5).Replace(':', '.'));
            s_hour[1] = float.Parse(student_hour.Substring(6, 5).Replace(':', '.'));
            foreach (Courses course in dbCourses)
            {
                if (course.day == student_day)
                {
                    c_hour[0] = float.Parse(course.hour.Substring(0, 5).Replace(':', '.'));
                    c_hour[1] = float.Parse(course.hour.Substring(6, 5).Replace(':', '.'));

                    if ((s_hour[0] >= c_hour[0] && s_hour[0] < c_hour[1]) || (s_hour[1] > c_hour[0] && s_hour[1] <= c_hour[1]) || (c_hour[0] >= s_hour[0] && c_hour[0] < s_hour[1]))
                    {
                        return false;
                    }
                }
            }
            return true;
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


        public ActionResult Exit()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

    }
}