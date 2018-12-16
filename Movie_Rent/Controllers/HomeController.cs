using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movie_Rent.Models;
using Movie_Rent.Dal;
using System.Data.Entity;

namespace Movie_Rent.Controllers
{
    public class HomeController : Controller
    {
        public List<Order> GetOrdersNotPaid(string UserName)
        {//get the list of Orders the user still not pay for them
            //connect to db
            OrderDal dal = new OrderDal();

            List<Order> orders = (from o in dal.Orders
                                  where o.UserName == UserName && o.Paid == "false"
                                  select o).ToList<Order>();
            return orders;
        }

        public List<Movie> MoviesByTypeValue(string TypeValue)
        {//get the list of Movies that have type the same as the TypeValue if exist in our users db 
            //connect to db
            MovieDal dal = new MovieDal();

            List<Movie> movies = (from m in dal.movies
                                  where m.Type == TypeValue
                                  select m).ToList<Movie>();
            return movies;
        }



        public List<Movie> MoviesBySearchValue(string SearchValue)
        {//get the list of Movies that contains the SearchValue, if exist in our users db
            //connect to db
            MovieDal dal = new MovieDal();

            List<Movie> movies = (from m in dal.movies
                       where m.MovieName.Contains(SearchValue)
                       select m).ToList<Movie>();


            return movies;
        }

        public Movie GetMovie(string MovieName)
        {//get the Movie by name
            //connect to db
            MovieDal dal = new MovieDal();

            Movie movie = (from m in dal.movies
                            where m.MovieName == MovieName
                            select m).FirstOrDefault();
            return movie;
        }


        public ActionResult HomePage()
        {//take us to the home page view

            //conecet to the movie database
            MovieDal dal = new MovieDal();

            //creating and saving the list of the movies
            MovieList moviesList = new MovieList();
            moviesList.movies = dal.movies.ToList();

            string searchValue = Request.Form["searchValue"];
            if (searchValue != null)
            {
                moviesList.movies = MoviesBySearchValue(searchValue);
            }

            return View(moviesList);
        }


        public ActionResult CategoryMovies(string category)
        {//take us to the home page view after serching movies with the type the user choose

            MovieList moviesList = new MovieList();
            moviesList.movies = MoviesByTypeValue(category);


            return View("HomePage", moviesList);
        }

        public ActionResult MovieInfo(string MovieName)
        {//the view that have the movie info

            Movie movie = GetMovie(MovieName);
            return View(movie);
        }

        public ActionResult MovieInfoPage(string MovieName)
        {//the same as the MovieInfo wuth a bottun to order the movie
            Movie movie = GetMovie(MovieName);
            return View(movie);
        }

        public ActionResult SendOrder(string MovieName)
        {//creat an order for the user and redircte to OrdersPage

            //connect to db
            OrderDal dal = new OrderDal();

            //get the max value of OrderNum
            int maxVal = (from o in dal.Orders
                          select o).Max(o => o.OrderNum);

            //creat the order
            Order order = new Order(maxVal+1, MovieName, Session["UserName"].ToString(), "false");

            //save order in db
            dal.Orders.Add(order);
            dal.SaveChanges();


            return RedirectToAction("OrdersPage");
        }

        public ActionResult OrdersPage()
        {//take us to the page view that will show us the user movies he orders byt still not paid to watch

            OrderList list = new OrderList();
            list.orders= GetOrdersNotPaid(Session["UserName"].ToString());

            return View(list);
        }

        public ActionResult RemoveOrder(int OrderNum)
        {//delet the order from our db

            //connect to db
            OrderDal dal = new OrderDal();

            //get the max value of OrderNum
            Order ord = (from o in dal.Orders
                         where o.OrderNum == OrderNum
                         select o).FirstOrDefault();

            //remove order in db
            dal.Orders.Remove(ord);
            dal.SaveChanges();


            return RedirectToAction("OrdersPage");
        }

        public ActionResult PayOrderView(int OrderNum, string MovieName)
        {
            ViewBag.OrderNum = OrderNum;
            ViewBag.MovieName = MovieName;

            return View();

        }

        public ActionResult PayOrder(int OrderNum)
        {//change the order value paid to true 

            //connect to db
            OrderDal dal = new OrderDal();

            //get the max value of OrderNum
            Order ord = (from o in dal.Orders
                         where o.OrderNum == OrderNum
                         select o).FirstOrDefault();

            //remove order in db
            ord.Paid = "true";
            ord.Date = DateTime.Now;
            dal.SaveChanges();


            return RedirectToAction("WatchListPage");
        }

        public void CheckWatchList( string UserName)
        {//checked if the movies that was paid by the user exceed 24 houers and if true remove them from order db

            //connect to db
            OrderDal dal = new OrderDal();

            //get the movies that was paid and exceed 24 houers
            List<Order> ord_list = (from o in dal.Orders
                         where DbFunctions.DiffHours(o.Date, DateTime.Now) >= 24
                         && o.UserName== UserName
                         && o.Paid == "true"
                         select o).ToList<Order>();


            //remove orders in db
            foreach (Order o in ord_list)
            { 
            dal.Orders.Remove(o);
            }

            //save changes
            dal.SaveChanges();

        }

        public ActionResult WatchListPage()
        {//take us to the page view that will show us the user novies he paid to watch

            //Check the user Watch List and update the list if neccery
            CheckWatchList(Session["UserName"].ToString());

            //connect to db Order 
            OrderDal dal = new OrderDal();
            List<Movie> movies_list = new List<Movie>();
            string UsreName = Session["UserName"].ToString();

            //get the movies that was paid by the user
            List<Order> ord_list = (from o in dal.Orders
                                    where o.UserName == UsreName
                                    && o.Paid == "true"
                                    select o).ToList<Order>();

            //creat the movie list with only the movies the user paid for
            foreach (Order o in ord_list)
            {
                movies_list.Add(GetMovie(o.MovieName));
            }

            MovieList list = new MovieList();
            list.movies = movies_list;

            return View(list);
        }


        public ActionResult Index()
        {

            return RedirectToAction("HomePage");
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}