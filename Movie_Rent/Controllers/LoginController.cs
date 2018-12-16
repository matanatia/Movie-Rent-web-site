using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movie_Rent.Models;
using Movie_Rent.Dal;

namespace Movie_Rent.Controllers
{
    public class LoginController : Controller
    {// controller for our first page - login page

        public User GetUserByName(string userName)
        {//get the user with the userName if exist in our users db
            //connect to db
            UserDal dal = new UserDal();

            User u1 = (from u in dal.users
                          where u.UserName == userName
                          select u).FirstOrDefault();


            return u1;
        }

        public bool checkUserName (string userName)
        {//check if the user name exist in our users db
            //connect to db
            UserDal dal = new UserDal();

            bool check = (from u in dal.users
                          where u.UserName == userName
                          select u).Any();
            if (check)
            {
                return true;
            }


            return false;
        }

        public bool checkUserPassword(string userName, string userPassword)
        {//check if the user name exist in our users db and if it have the password  userPassword
            //connect to db
            UserDal dal = new UserDal();

            bool check = (from u in dal.users
                          where u.UserName == userName && u.Password == userPassword
                          select u).Any();
            if (check)
            {
                return true;
            }


            return false;
        }

        public ActionResult LoginPage(User u1)
        {//the form at our login page

            bool loginNameCheck = checkUserName(u1.UserName);

            if (loginNameCheck && u1.UserName!=null)
            {// chack if the name of the user exist in the db
                bool loginPasswordCheck = checkUserPassword(u1.UserName, u1.Password);
                if (loginPasswordCheck)
                {//check if the user enter the right password

                    //getting the user info from the db
                    User u2 = GetUserByName(u1.UserName);
                    //saving the user info we need for next actions
                    Session["UserType"] = u2.Type;
                    Session["UserName"] = u2.UserName;
                    return RedirectToAction("HomePage", "Home");
                }
                ViewBag.message2 = "The password is incorrect"; 

            }
            else if (u1.UserName != null) { ViewBag.message1 = "The user name not exist"; }
            else if (u1.UserName == null) { ViewBag.message1 = ""; ViewBag.message2 = ""; }

            return View(u1); 
        }

        public ActionResult RegistrationPage(User u1)
        {//the form at our registration page

            bool loginNameCheck = checkUserName(u1.UserName);

            if (ModelState.IsValid && !loginNameCheck)
            {
                //connect to db
                UserDal dal = new UserDal();
                dal.users.Add(u1);
                dal.SaveChanges();

                //saving the user info we need for next actions
                Session["UserType"] = "user";
                Session["UserName"] = u1.UserName;
                return RedirectToAction("HomePage", "Home");
            }
            else if (loginNameCheck) { ViewBag.message1 = "That user name already exist"; }
            return View(u1);
        }
    }
}