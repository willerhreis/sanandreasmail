using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SanAndreasMail.Domain;
using SanAndreasMail.Domain.Models;
using SanAndreasMail.Domain.Services;
using SanAndreasMail.Infra.Helpers;
using SanAndreasMail.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.IO;
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

                string routeSectionsFilePath = Console.ReadLine();

                //TODO: Validate file pattern of route section
                List<string> routeSectionsText = Utility.ReadFile(routeSectionsFilePath);

                //Get route sections by file and save in database
                _routeSectionService.GetRouteSections(routeSectionsText);

                Console.WriteLine("\n\nPor favor, informe o caminho do arquivo de Encomendas: ");

                string orderFilePath = Console.ReadLine();

                //TODO: Validate file pattern of order route
                List<string> ordersText = Utility.ReadFile(orderFilePath);

                //Get orders by file
                List<Order> orders = _orderService.GetOrders(ordersText).Result;

                Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine("\n\nPor favor, informe a pasta para salvar as Rotas: ");

                string path = Console.ReadLine();

                path += @"\rotas.txt";

                if (path != "" && File.Exists(path))
                    File.Delete(path);


                foreach (Order order in orders)
                {
                    List<Route> routes = GetShortestRouteFromOrders(order);
                    GenerateOutputFile(routes, path);
                }

                Console.WriteLine("----------------------------------------------------------");

                Console.WriteLine("\n\nArquivo de saída gerado: " + path);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("\n\nAperte uma tecla para sair...");
            Console.ReadKey();
            Environment.Exit(0);
        }

        /// <summary>
        /// Generate output file with the routes
        /// </summary>
        /// <param name="routes"></param>
        /// <param name="path"></param>
        private static void GenerateOutputFile(List<Route> routes, string path)
        {
            int totalTime = 0;
            string printContent = "";
            City auxDestinyRoute = new City();

            for (int routeCount = 0; routeCount < routes.Count; routeCount++)
            {
                //Get total of time
                totalTime += routes[routeCount].TotalTravelTime;

                //Generate content of routes.
                //if the last destination is the new source, it does not write to the file
                printContent += (routes[routeCount].Origin != auxDestinyRoute ? routes[routeCount].Origin.Abbreviation : "") + " " + routes[routeCount].Destiny.Abbreviation;
                
                //Save the the last destination
                auxDestinyRoute = routes[routeCount].Destiny;
            }

            string formatedContent = printContent + " " + totalTime;

            //Save to file
            Utility.WriteFile(formatedContent, path);

        }

        /// <summary>
        /// Generate the shortest route by Order and return list of routes
        /// </summary>
        /// <param name="order"></param>
        /// <returns>list of routes</returns>
        private static List<Route> GetShortestRouteFromOrders(Order order)
        {
            var routes = _routeService.GetShortestRoute(order).Result;

            if (routes.Count > 0)
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine("\n| Melhor Rota para: " + order.Origin.Name + " ----> " + order.Destiny.Name);
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

            return routes;

        }



        /// <summary>
        /// Load data of System
        /// </summary>
        private static async void InitSystemData()
        {
            IEnumerable<City> cities = await _cityService.ListAsync();

            // Caution: Delete all rows of RouteSection table because the propose is only to show the behavior 
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
