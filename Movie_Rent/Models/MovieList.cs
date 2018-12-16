using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie_Rent.Models
{
    public class MovieList
    {// class that contains movies list

        public List<Movie> movies { get; set; }

        public MovieList()
        {
            movies = new List<Movie>();
        }
    }
}