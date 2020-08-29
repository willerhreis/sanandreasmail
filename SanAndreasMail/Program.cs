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
using System.Text;

namespace SanAndreasMail
{
    class Program
    {

        private static ICityService _cityService;
        private static AppDbContext _context;

        static void Main()
        {
            try
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("--- Bem vindo ao correio de San Andreas ---");
                Console.WriteLine("-------------------------------------------");

                Console.WriteLine("\n\nCarregando os serviços do sistema... ");
                var startUp = new StartUp();
                startUp.ConfigureServices();

                _cityService = startUp.serviceProvider.GetService<ICityService>();
                _context = startUp.serviceProvider.GetService<AppDbContext>();
                _context.Database.EnsureCreated();

                Console.WriteLine("Carregando os dados do sistema... ");

                InitSystemData();

                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("\n\nPor favor, informe o caminho do arquivo com Trechos das Rotas: ");

                string routesSectionFilePath = Console.ReadLine();

                Utility.ReadFile(routesSectionFilePath);

                Console.WriteLine("\n\nPor favor, informe o caminho do arquivo de Encomendas: ");

                string orderFilePath = Console.ReadLine();

                Utility.ReadFile(orderFilePath);


            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();

        }

        /// <summary>
        /// Load data of System
        /// </summary>
        private static async void InitSystemData()
        {
            IEnumerable<City> cities = await _cityService.ListAsync();

            if (cities.Count() > 0)
            {
                Console.WriteLine("\n\nCidades:");
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
