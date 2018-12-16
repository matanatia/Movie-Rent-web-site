using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Movie_Rent.Models;

namespace Movie_Rent.Dal
{
    public class MovieDal : DbContext
    {// conect us to the the Movies DB

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>().ToTable("Movies");
        }

        public DbSet<Movie> movies { get; set; }
    }
}