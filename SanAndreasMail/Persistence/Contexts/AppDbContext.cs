using Microsoft.EntityFrameworkCore;
using SanAndreasMail.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanAndreasMail.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<RouteSection> RouteSections { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<City>().HasKey(a => a.Id);
            builder.Entity<Route>().HasKey(a => a.Id);
            builder.Entity<RouteSection>().HasKey(a => a.Id);
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=sanandreasmail.db");

    }
}
