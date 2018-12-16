using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie_Rent.Models
{
    public class OrderList
    {// class that contains orders list

        public List<Order> orders { get; set; }

        public OrderList()
        {
            orders = new List<Order>();
        }
    }
}