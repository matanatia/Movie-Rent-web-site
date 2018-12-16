using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Movie_Rent.Models;

namespace Movie_Rent.Dal
{
    public class UserDal : DbContext
    {// conect us to the the Users DB

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users");
        }

        public DbSet<User> users { get; set; }
    }
}