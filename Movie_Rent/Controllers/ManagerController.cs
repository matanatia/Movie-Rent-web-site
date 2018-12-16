using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movie_Rent.Models;
using Movie_Rent.Dal;


namespace Movie_Rent.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
        public ActionResult ManagerZonePage()
        {//take us to the manager zone Page view

            return View();
        }

        public ActionResult UsersListPage()
        {//take us to the users list page view

            //connect to db users
            UserDal dal = new UserDal();

            UserList list = new UserList();
            list.users= (from u in dal.users
                          select u).ToList<User>();

            return View(list);
        }


        public ActionResult RemoveUser(string UserName)
        {//remove the user with the same name in the db and return us to Users List Page

            //connect to db
            UserDal dal = new UserDal();

            //get the user from db
            User u1 = (from u in dal.users
                       where u.UserName == UserName
                       select u).FirstOrDefault();

            //remove the user from db
            dal.users.Remove(u1);
            dal.SaveChanges();

            return RedirectToAction("UsersListPage");
        }

        public bool checkUserName(string userName)
        {//check if the user name exist in our users db
            //connect to db
            UserDal dal = new UserDal();

            bool check = (from u in dal.users
                          where u.UserName == userName
                          select u).Any();

            return check;
        }

        public ActionResult AddUserPage(User u1)
        {//take us to the Add user page view

            bool NameCheck = checkUserName(u1.UserName);

            if (Request.Form["option"] == null)
            {
                ViewBag.message2 = "you diden't checked Manger/User option";
                return View(u1);
            }

            u1.Type = Request.Form["option"].ToString();

            if (ModelState.IsValid && !NameCheck)
            {
                //connect to db
                UserDal dal = new UserDal();
                dal.users.Add(u1);
                dal.SaveChanges();

                return RedirectToAction("ManagerZonePage");
            }
            else if (NameCheck) { ViewBag.message1 = "That user name already exist"; }
            return View(u1);
        }

        public ActionResult MovieListPage()
        {//take us to the movie list page view

            //connect to db movies
            MovieDal dal = new MovieDal();

            MovieList list = new MovieList();
            list.movies = (from m in dal.movies
                          select m).ToList<Movie>();

            return View(list);
        }

        public ActionResult RemoveMovie(string MovieName)
        {//remove the movie with the same name in the db and return us to Movie List Page

            //connect to db
            MovieDal dal = new MovieDal();

            //get the movie from db
            Movie m1 = (from m in dal.movies
                        where m.MovieName == MovieName
                        select m).FirstOrDefault();

            //remove the movie from db
            dal.movies.Remove(m1);
            dal.SaveChanges();

            return RedirectToAction("MovieListPage");
        }

        public ActionResult EditMovie(string MovieName)
        {//get the movie we want to add from db and send him to AddMoviePage

            //connect to db
            MovieDal dal = new MovieDal();

            //get the movie from db
            Movie m1 = (from m in dal.movies
                        where m.MovieName == MovieName
                        select m).FirstOrDefault();

            return View ("EditMoviePage", m1);
        }

        public ActionResult EditMoviePage(Movie m1)
        {//take us to the edit movie page view - in the edit action we need to chack only if ModelState.IsValid

            if (ModelState.IsValid)
            {
                //connect to db
                MovieDal dal = new MovieDal();

                //get the max value of OrderNum
                Movie m2 = (from m in dal.movies
                             where m.MovieName== m1.MovieName
                             select m).FirstOrDefault();

                //edit movie in db
                m2.MovieName = m1.MovieName;
                m2.Type= m1.Type;
                m2.Description = m1.Description;
                m2.Link = m1.Link;
                m2.ImageLink = m1.ImageLink;

                dal.SaveChanges();

                return RedirectToAction("MovieListPage");
            }

            return View(m1);

        }

        public bool checkMovieName(string MovieName)
        {//check if the movie name exist in our users db
            //connect to db
            MovieDal dal = new MovieDal();

            bool check = (from m in dal.movies
                          where m.MovieName == MovieName
                          select m).Any();

            return check;
        }

        public ActionResult AddMoviePage(Movie m1)
        {//take us to the add movie page view

            bool NameCheck = checkMovieName(m1.MovieName);

            if (ModelState.IsValid && !NameCheck)
            {
                //save date of creating the movie in ouer db
                m1.Date = DateTime.Now;

                //connect to db
                MovieDal dal = new MovieDal();
                dal.movies.Add(m1);
                dal.SaveChanges();

                return RedirectToAction("MovieListPage");
            }
            else if (NameCheck) { ViewBag.message1 = "That movie name already exist"; }
            return View(m1);
        }
        public ActionResult OrderListPage()
        {//take us to the order list page view

            //connect to db order
            OrderDal dal = new OrderDal();

            OrderList list = new OrderList();
            list.orders = (from o in dal.Orders
                           select o).ToList<Order>();

            return View(list);
        }
    }
}