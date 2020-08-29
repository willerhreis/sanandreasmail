using SanAndreasMail.Domain;
using SanAndreasMail.Domain.Respositories;
using SanAndreasMail.Infra;
using SanAndreasMail.Persistence.Contexts;
using SanAndreasMail.Persistence.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SanAndreasMail
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Welcome to San Andreas Post Office");
            Console.WriteLine("----------------------------------");

            Console.WriteLine("Loading Cities... ");
            InitDatabase();


            Console.ReadKey();
        }

        private static async void InitDatabase()
        {
            using var context = new AppDbContext();
            context.Database.EnsureCreated();

            ICityRepository _cityRepository = new CityRespository(context);

            IEnumerable<City> cities = await _cityRepository.ListAsync();

            if (cities.Count() > 0)
                foreach (City city in cities)
                    Console.WriteLine(city.ToString());
        }

    }
}
