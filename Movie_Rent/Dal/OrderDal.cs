using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Movie_Rent.Models;

namespace Movie_Rent.Dal
{
    public class OrderDal : DbContext
    {// conect us to the the Orders DB
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>().ToTable("Orders");
        }

        public DbSet<Order> Orders { get; set; }
    }
}