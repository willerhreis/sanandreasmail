using Microsoft.EntityFrameworkCore;
using SanAndreasMail.Domain;
using SanAndreasMail.Infra.Helpers;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SanAndreasMail.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<RouteSection> RouteSections { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<City>().HasKey(a => a.CityId);
            builder.Entity<City>().Property(p => p.CityId).ValueGeneratedOnAdd();
            builder.Entity<Route>().HasKey(a => a.RouteId);
            builder.Entity<Route>().Property(p => p.RouteId).ValueGeneratedOnAdd();
            builder.Entity<RouteSection>().HasKey(a => a.RouteSectionId);
            builder.Entity<RouteSection>().Property(p => p.RouteSectionId).ValueGeneratedOnAdd();

            //Initiate Database with seed
            builder.Seed();

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /* string db = Utility.GetConnectionString("ConnectionStrings:DefaultConnection");
             optionsBuilder.UseInMemoryDatabase(db);*/
        }

    }
}
