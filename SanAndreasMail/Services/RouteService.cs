﻿using SanAndreasMail.Domain;
using SanAndreasMail.Domain.Models;
using SanAndreasMail.Domain.Respositories;
using SanAndreasMail.Domain.Services;
using SanAndreasMail.Infra.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanAndreasMail.Services
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _routeRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IRouteSectionRepository _routeSectionsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RouteService(IUnitOfWork unitOfWork,
            IRouteRepository routeRepository, IRouteSectionRepository routeSectionsRepository, ICityRepository cityRepository)
        {
            _routeRepository = routeRepository;
            _routeSectionsRepository = routeSectionsRepository;
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<Route> FindByIdAsync(Guid id)
        {
            return await _routeRepository.FindByIdAsync(id);
        }

        public async Task<IEnumerable<Route>> ListAsync()
        {
            return await _routeRepository.ListAsync();
        }

        public async Task<Route> SaveAsync(Route route)
        {
            try
            {
                await _routeRepository.AddAsync(route);
                await _unitOfWork.CompleteAsync();

                return route;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred when creating the route: {ex.Message}");
            }
        }

        public Task<Route> UpdateAsync(Guid id, Route route)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the shortest of route between two cities
        /// </summary>
        /// <param name="route"></param>
        public async Task<List<Route>> GetShortestRoute(Order order)
        {
            if (order == null)
                throw new Exception("Invalid arguments.");

            IEnumerable<City> cities = await _cityRepository.ListAsync();
            IEnumerable<RouteSection> routeSections = await _routeSectionsRepository.ListAsync();
            List<Node> nodes = new List<Node>();
            List<Route> listRoutes = new List<Route>();


            //Create nodes
            foreach (City city in cities)
            {
                nodes.Add(new Node(city.Abbreviation, city.CityId));
            }

            //Connect nodes
            foreach (Node node in nodes)
            {
                //get routes by node
                List<RouteSection> routesByNode = routeSections.Where(a => a.Origin == node.Id).ToList();

                //Get destiny from routes
                foreach (RouteSection destiny in routesByNode)
                {
                    //Get destiny node like route section destinny
                    Node nodeDestiny = nodes.Where(a => a.Id == destiny.Destiny).FirstOrDefault();

                    //Connect node to node destiny
                    node.ConnectTo(nodeDestiny, destiny.TravelTime);
                }
            }

            //Get node origin by Order
            Node nodeOrderOrigin = nodes.Where(a => a.Id == order.Origin.CityId).FirstOrDefault();
            //Get node destiny by Order
            Node nodeOrderDestiny = nodes.Where(a => a.Id == order.Destiny.CityId).FirstOrDefault();

            //Find the shortest path between origin node and destiny node
            Node[] nodeRoutes = FindShortestPath(nodeOrderOrigin, nodeOrderDestiny);

            return GetRoutesFromNodes(nodeRoutes, routeSections, cities);
        }


        /// <summary>
        /// Find shortest Path between two nodes
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        /// <returns></returns>
        private Node[] FindShortestPath(Node node1, Node node2)
        {
            IShortestPathFinder shortestPath = new Dijkstra();
            Node[] nodeRoutes = shortestPath.FindShortestPath(node1, node2);

            return nodeRoutes;
        }

        /// <summary>
        /// Get list of routes by return of Dijkstra algorithim
        /// </summary>
        /// <param name="nodeRoutes"></param>
        /// <param name="routeSections"></param>
        /// <param name="cities"></param>
        /// <returns>list of routes</returns>
        private List<Route> GetRoutesFromNodes(Node[] nodeRoutes, IEnumerable<RouteSection> routeSections, IEnumerable<City> cities )
        {
            List<Route> listOfRoutes = new List<Route>();
            for (int nodeCount = 0; nodeCount < nodeRoutes.Length - 1; nodeCount++)
            {
                City cityOrigin = cities.Where(a => a.CityId == nodeRoutes[nodeCount].Id).FirstOrDefault();
                City cityDestiny = cities.Where(a => a.CityId == nodeRoutes[nodeCount + 1].Id).FirstOrDefault();

                Route finalRoute = new Route
                {
                    OriginId = cityOrigin.CityId,
                    Origin = cityOrigin,
                    DestinyId = cityDestiny.CityId,
                    Destiny = cityDestiny,
                    TotalTravelTime = routeSections.Where(a => a.Origin == cityOrigin.CityId && a.Destiny == cityDestiny.CityId).Select(t => t.TravelTime).FirstOrDefault()
                };

                listOfRoutes.Add(finalRoute);
            }

            return listOfRoutes;
        }
    }
}
