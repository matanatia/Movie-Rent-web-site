using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movie_Rent.Models
{
    public class Movie
    {// class that continas our movie info

        public string Type { get; set; }

        [Key]
        [Required(ErrorMessage = "Movie name is required")]
        public string MovieName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(400, ErrorMessage = "The description length should be up to 400 characters")]
        public string Description { get; set;}

        [Required(ErrorMessage = "Link is required")]
        public string Link { get; set; }

        public DateTime Date { get; set; }

        public string ImageLink { get; set; }

        public Movie() { }

        public Movie(string Type = null, string MovieName = null, string Description = null, string Link = null)
        {
            this.Type = Type;
            this.MovieName = MovieName;
            this.Description = Description;
            this.Link = Link;
            Date = DateTime.Now;
            ImageLink = "http://3.bp.blogspot.com/-5kZ-JZPQUP8/VC90TGDiD1I/AAAAAAAAAxY/r7RSunA5LF8/s1600/show.jpg";
        }

    }
}