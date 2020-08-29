using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SanAndreasMail.Domain;
using SanAndreasMail.Domain.Respositories;
using SanAndreasMail.Domain.Services;
using SanAndreasMail.Infra;
using SanAndreasMail.Infra.Helpers;
using SanAndreasMail.Persistence.Contexts;
using SanAndreasMail.Persistence.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SanAndreasMail
{
    class Program
    {

        private static ICityService _cityService;
        private static AppDbContext _context;

        static void Main(string[] args)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Welcome to San Andreas Post Office");
            Console.WriteLine("----------------------------------");


            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(Utility.GetConnectionString("ConnectionStrings:DefaultConnection"));
            });

            services.AddMemoryCache();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddScoped<IRouteSectionRepository, RouteSectionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICityService, CityService>();

            var serviceProvider = services.BuildServiceProvider();
            _cityService = serviceProvider.GetService<ICityService>();
            _context = serviceProvider.GetService<AppDbContext>();

            _context.Database.EnsureCreated();

            Console.WriteLine("Loading System Data... ");


            InitSystemData();


            Console.ReadKey();
        }

        private static async void InitSystemData()
        {

            IEnumerable<City> cities = await _cityService.ListAsync();

            if (cities.Count() > 0)
            {
                Console.WriteLine("Cities:");
                foreach (City city in cities)
                    Console.WriteLine(city.ToString());
            }
            else
            {
                Console.WriteLine("No cities added");
            }
        }

    }
}
