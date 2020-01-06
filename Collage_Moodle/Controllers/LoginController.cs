using Collage_Moodle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Collage_Moodle.Dal;

namespace Collage_Moodle.Controllers
{
    public class LoginController : Controller
    {
        private PermissionController perm = new PermissionController();
        public ActionResult Index()
        {
            if (Session["user"] == null)
                return View("LoginView");
            else
                return perm.CheckPermission((Users)Session["user"]);
        }

        [HttpPost]
        public ActionResult Submit()
        {

            DAL dal = new DAL();

            string userName = Request.Form["usernameInput"].ToString();
            string password = Request.Form["passwordInput"].ToString();
            var dbUser = dal.Users.Single<Users>(x => x.userName == userName && x.password == password);
            if (dbUser != null)
            /*
            List<Users> usersList =
                (from x in dal.Users
                 where (x.userName.Equals(userName) & x.password.Equals(password))
                 select x).ToList<Users>();

            TempData["loggedUser"] = usersList[0].userName;

            if (usersList.Count > 0)
              */
            {
                Session["user"] = new Users { userName = userName, password = password, permission = dbUser.permission};
                return perm.CheckPermission((Users)Session["user"]);
            }
            else
                return View("LoginView");


        }




        /*
        string userName = Request.Form["usernameInput"].ToString();
        string password = Request.Form["passwordInput"].ToString();




            //Users user = null;

            try
            {
                var userDetails = db.Users.Where(x => x.userName == userName && x.password == password).FirstOrDefault();
                if (userDetails != null)
                {
                    Session["user"] = new Users { userName = userDetails.userName, permission = userDetails.permission };
                    return perm.CheckPermission((Users)Session["user"]);
                }
                else
                    return View("LoginView");
            }
            catch
            {
                Console.WriteLine("It was not possible to enter the database.");
            }

            //checking if the user and the password are correct.

        }
        return View("LoginView");

*/

    }   
}

/*
            public ActionResult Submit(UserModel user)
{

    UserDal dal = new UserDal();

    string name = user.FirstName.ToString();
    string ID = user.UserID.ToString();
    string password = user.Password.ToString();
    List<UserModel> usersList =
        (from x in dal.users
         where (x.FirstName.Equals(name) & x.UserID.Equals(ID) & x.Password.Equals(password))
         select x).ToList<UserModel>();

    TempData["loggedUser"] = usersList[0].UserID;
    //  ViewBag.logged = usersList[0].UserID;
    Session["UserID"] = ID;
    if (usersList.Count > 0)
        if (usersList[0].Type.Contains("student"))
            return View("StudentPage");
        else
            return View("LecturerPage");
    else
        return View("ErrorPage");
}
    }
*/
