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


            Console.WriteLine("Loading System Services... ");
            var startUp = new StartUp();
            startUp.ConfigureServices();

            _cityService = startUp.serviceProvider.GetService<ICityService>();
            _context = startUp.serviceProvider.GetService<AppDbContext>();
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
