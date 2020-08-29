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
                options.UseInMemoryDatabase(Utility.GetConnectionString("ConnectionStrings:DefaultConnection"));
            });

            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddScoped<IRouteSectionRepository, RouteSectionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICityService, CityService>();

            var serviceProvider = services.BuildServiceProvider();
            _cityService = serviceProvider.GetService<ICityService>();
            _context = serviceProvider.GetService<AppDbContext>();

            Console.WriteLine("Loading Cities... ");

            InitDatabase();


            Console.ReadKey();
        }

        private static async void InitDatabase()
        {

            IEnumerable<City> cities = await _cityService.ListAsync();

            if (cities.Count() > 0)
                foreach (City city in cities)
                    Console.WriteLine(city.ToString());
        }

    }
}
