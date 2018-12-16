using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movie_Rent.Models
{
    public class Order
    {// class that continas the info for an order of a movie rent by the user

        [Key]
        public int OrderNum { get; set; }

        [Required(ErrorMessage = "Movie name is required")]
        public string MovieName { get; set; }

        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }

        public DateTime Date { get; set; }

        //for us to know if the user paid for its order or not
        [Required(ErrorMessage = "Paid is required")]
        public string Paid { get; set; }

        public Order() { }

        public Order(int OrderNum, string MovieName = null, string UserName = null, string Paid = null)
        {
            this.OrderNum = OrderNum;
            this.MovieName = MovieName;
            this.UserName = UserName;
            Date = DateTime.Now;
            this.Paid = Paid;
        }
    }
}