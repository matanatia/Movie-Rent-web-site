using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie_Rent.Models
{
    public class UserList
    {// class that contains user list

        public List <User> users { get; set; }

        public UserList()
        {
            users = new List<User>();
        }
    }
}