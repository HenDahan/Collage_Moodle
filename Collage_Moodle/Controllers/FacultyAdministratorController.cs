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
                if (check_student(studentID, courseName))
                {


                    dal.Students.Add(new Students { Courses_cName = courseName, Users_userID = studentID });
                    try
                    {
                        dal.SaveChanges();
                    }
                    catch
                    {
                        TempData["Message"] = "Assigned Failed, The student in this course already exists.";
                        return perm.CheckPermission(user);

                    }
                    TempData["Message"] = "Assigned successfully";
                    return perm.CheckPermission(user);
                }
                else
                {
                    TempData["Message"] = "Failed - The student cannot study two courses at the same time!";
                    return perm.CheckPermission(user);
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
                string courseName = update_grade.courseName;
                int userID = update_grade.studentID;
                int new_grade = update_grade.grade;
                List<Students> dbStudent = (from x in dal.Students
                                            where (x.Users_userID.Equals(userID) && x.Courses_cName.Equals(courseName))
                                            select x).ToList<Students>();
                if (dbStudent.Count > 0)
                {
                    Students tempStudent = dal.Students.Single<Students>(x => x.Courses_cName == courseName && x.Users_userID == userID);
                    tempStudent.grade = new_grade;
                    dal.SaveChanges();
                    TempData["Message"] = "Grade Updated successfully";
                    return perm.CheckPermission(user);
                }
                else
                {
                    TempData["Message"] = "There is no such user ID studing that course.";
                    return perm.CheckPermission(user);
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
                string new_date = update_exam.new_date;
                string new_classroom = update_exam.new_classroom;
                List<Exams> dbExam = (from x in dal.Exams
                                    where (x.Courses_cName.Equals(courseName) && x.moed.Equals(moed))
                                       select x).ToList<Exams>();
                if (dbExam.Count > 0)
                {
                    Exams tempExam = dal.Exams.Single<Exams>(x => x.Courses_cName == courseName && x.moed == moed);
                    tempExam.date = new_date;
                    tempExam.classroom = new_classroom;

                    dal.SaveChanges();
                    TempData["Message"] = "The exam date and classroom UPDATED successfully.";
                    return perm.CheckPermission(user);
                }
                else
                {
                    dal.Exams.Add(new Exams { Courses_cName = courseName, moed = moed, date = new_date, classroom = new_classroom });
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
                    if (check_lecturer(lecturerID, day, hour, 1))
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
                        return perm.CheckPermission(user);
                    }
                }
                else
                {
                    if (check_lecturer(lecturerID, day, hour, 1))
                    {
                        dal.Courses.Add(new Courses { courseName = courseName, day = day, hour = hour, classroom = classroom, Users_lecturerID = lecturerID });
                        dal.SaveChanges();
                        TempData["Message"] = "The course data has been CREATED successfully.";
                        return perm.CheckPermission(user);
                    }
                    else
                    {
                        TempData["Message"] = "Error - The lecturer cannot lecture another course at the same time!";
                        return perm.CheckPermission(user);
                    }
                }
            }
        }

        private bool check_lecturer(int id, string day, string hour, int type)
        {

            List<Courses> dbCourses = (from x in dal.Courses
                                        where (x.Users_lecturerID.Equals(id))
                                        select x).ToList<Courses>();

            float[] l_hour = new float[2];
            float[] c_hour = new float[2];
            l_hour[0] = float.Parse(hour.Substring(0, 5).Replace(':', '.'));
            l_hour[1] = float.Parse(hour.Substring(9, 5).Replace(':', '.'));
            foreach (Courses course in dbCourses)
            {
                if (course.day == day)
                {
                    c_hour[0] = float.Parse(course.hour.Substring(0, 5).Replace(':', '.'));
                    c_hour[1] = float.Parse(course.hour.Substring(9, 5).Replace(':', '.'));

                    if ((l_hour[0] > c_hour[0] && l_hour[0] < c_hour[1]) || (l_hour[1] > c_hour[0] && l_hour[1] < c_hour[1]) || (c_hour[0] > l_hour[0] && c_hour[0] < l_hour[1]))
                    {
                        return false;
                    }
                }
            }
                return true;
        }

        private bool check_student(int id, string courseName)
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
            s_hour[1] = float.Parse(student_hour.Substring(9, 5).Replace(':', '.'));
            foreach (Courses course in dbCourses)
            {
                if (course.day == student_day)
                {
                    c_hour[0] = float.Parse(course.hour.Substring(0, 5).Replace(':', '.'));
                    c_hour[1] = float.Parse(course.hour.Substring(9, 5).Replace(':', '.'));

                    if ((s_hour[0] > c_hour[0] && s_hour[0] < c_hour[1]) || (s_hour[1] > c_hour[0] && s_hour[1] < c_hour[1]) || (c_hour[0] > s_hour[0] && c_hour[0] < s_hour[1]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public ActionResult Exit()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

    }
}