using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SanAndreasMail.Domain;
using SanAndreasMail.Domain.Models;
using SanAndreasMail.Domain.Services;
using SanAndreasMail.Infra.Helpers;
using SanAndreasMail.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SanAndreasMail
{
    class Program
    {

        private static IRouteSectionService _routeSectionService;
        private static ICityService _cityService;
        private static IRouteService _routeService;
        private static IOrderService _orderService;
        private static AppDbContext _context;

        static void Main()
        {
            try
            {
                Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine("----------- Bem vindo ao correio de San Andreas -----------");
                Console.WriteLine("-----------------------------------------------------------");

                Console.WriteLine("\n\nCarregando os serviços do sistema... ");
                var startUp = new StartUp();
                startUp.ConfigureServices();

                _cityService = startUp.serviceProvider.GetService<ICityService>();
                _orderService = startUp.serviceProvider.GetService<IOrderService>();
                _routeSectionService = startUp.serviceProvider.GetService<IRouteSectionService>();
                _routeService = startUp.serviceProvider.GetService<IRouteService>();
                _context = startUp.serviceProvider.GetService<AppDbContext>();
                _context.Database.EnsureCreated();

                Console.WriteLine("Carregando os dados do sistema... ");

                InitSystemData();

                Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine("\n\nPor favor, informe o caminho do arquivo com Trechos das Rotas: ");

                //string routeSectionsFilePath = Console.ReadLine();

                string routeSectionsFilePath = @"D:\Projetos\Eu Programador\SanAndreasMail\ArquivosEntrada\trechos.txt";

                //TODO: Validate file pattern of route section
                List<string> routeSectionsText = Utility.ReadFile(routeSectionsFilePath);

                //Get route sections by file
                _routeSectionService.GetRouteSections(routeSectionsText);

                Console.WriteLine("\n\nPor favor, informe o caminho do arquivo de Encomendas: ");

                //string orderFilePath = Console.ReadLine();
                string orderFilePath = @"D:\Projetos\Eu Programador\SanAndreasMail\ArquivosEntrada\encomendas.txt";

                //TODO: Validate file pattern of order route
                List<string> ordersText = Utility.ReadFile(orderFilePath);

                //Get orders by file
                List<Order> orders = _orderService.GetOrders(ordersText).Result;

                foreach (Order order in orders)
                {
                    var routes = _routeService.GetShortestRoute(order).Result;

                    if (routes.Count > 0)
                    {
                        Console.WriteLine("----------------------------------------------------------");
                        Console.WriteLine("\n| Melhor Rota para: " + order.Origin + " ----> " + order.Destiny);
                        Console.WriteLine("----------------------------------------------------------");

                        int totalTime = 0;
                        foreach (Route route in routes)
                        {
                            totalTime += route.TotalTravelTime;
                            Console.WriteLine(route.ToString());
                            Console.WriteLine("----------------------------------------------------------");
                        }
                        Console.WriteLine("| Tempo total da rota: " + totalTime + " dia(s)                          |");
                    }
                }

                Console.WriteLine("----------------------------------------------------------");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Aperte uma tecla para sair...");
            Console.ReadKey();
            System.Environment.Exit(0);
        }



        /// <summary>
        /// Load data of System
        /// </summary>
        private static async void InitSystemData()
        {
            IEnumerable<City> cities = await _cityService.ListAsync();

            // Caution: Delete all rows of route section because the propose is only to show the behavior 
            // on use a real database. This is not the best practice under any case. 
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM RouteSections");

            if (cities.Count() > 0)
            {
                Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine("\n\nCidades:");
                Console.WriteLine("-----------------------------------------------------------");

                foreach (City city in cities)
                    Console.WriteLine(city.ToString());
            }
            else
            {
                Console.WriteLine("Nenhuma cidade existente.");
            }
        }

    }
}
