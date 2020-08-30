using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using SanAndreasMail.Domain;
using SanAndreasMail.Domain.Models;
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
using System.Threading.Tasks;

namespace SanAndreasMail
{
    class Program
    {

        private static IRouteSectionService _routeSectionService;
        private static ICityService _cityService;
        private static IOrderService _orderService;
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
                _orderService = startUp.serviceProvider.GetService<IOrderService>();
                _routeSectionService = startUp.serviceProvider.GetService<IRouteSectionService>();
                _context = startUp.serviceProvider.GetService<AppDbContext>();
                _context.Database.EnsureCreated();

                Console.WriteLine("Carregando os dados do sistema... ");

                InitSystemData();

                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("\n\nPor favor, informe o caminho do arquivo com Trechos das Rotas: ");

                string routeSectionsFilePath = Console.ReadLine();

                //TODO: Validate file pattern of route section
                List<string> routeSectionsText = Utility.ReadFile(routeSectionsFilePath);

                //Get route sections by file
                _routeSectionService.GetRouteSections(routeSectionsText);

                Console.WriteLine("\n\nPor favor, informe o caminho do arquivo de Encomendas: ");

                string orderFilePath = Console.ReadLine();

                //TODO: Validate file pattern of order route
                List<string> ordersText = Utility.ReadFile(orderFilePath);

                //Get orders by file
                _orderService.GetOrders(ordersText);


            }
            catch(Exception e)
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
