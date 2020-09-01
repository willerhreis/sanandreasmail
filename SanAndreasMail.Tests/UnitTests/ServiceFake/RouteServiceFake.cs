using SanAndreasMail.Domain;
using SanAndreasMail.Domain.Models;
using SanAndreasMail.Domain.Services;
using SanAndreasMail.Infra.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanAndreasMail.Tests.UnitTests.ServiceFake
{
    public class RouteServiceFake : IRouteService
    {
        public List<Route> _routes { get; set; }
        private ICityService _cityServiceFake;
        private IRouteSectionService _routeSectionServiceFake;

        public RouteServiceFake(RouteSectionServiceFake routeSectionServiceFake, CityServiceFake cityServiceFake)
        {
            _cityServiceFake = cityServiceFake;
            _routeSectionServiceFake = routeSectionServiceFake;
        }

        public async Task<IEnumerable<Route>> ListAsync()
        {
            return await Task.Run(() => _routes);
        }

        public async Task<Route> SaveAsync(Route route)
        {
            route.RouteId = Guid.NewGuid();
            await Task.Run(() => _routes.Add(route));
            return route;
        }

        public async Task<Route> FindByIdAsync(Guid id)
        {
            return await Task.Run(() => _routes.Where(a => a.RouteId == id).FirstOrDefault());
        }

        public Task<Route> UpdateAsync(Guid id, Route route)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Route>> GetShortestRoute(Order order)
        {
            if (order == null)
                throw new Exception("Invalid arguments.");

            IEnumerable<City> cities = await _cityServiceFake.ListAsync();

            IEnumerable<RouteSection> routeSections = await _routeSectionServiceFake.ListAsync();
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

        
        private Node[] FindShortestPath(Node node1, Node node2)
        {
            IShortestPathFinder shortestPath = new Dijkstra();
            Node[] nodeRoutes = shortestPath.FindShortestPath(node1, node2);

            return nodeRoutes;
        }

        
        private List<Route> GetRoutesFromNodes(Node[] nodeRoutes, IEnumerable<RouteSection> routeSections, IEnumerable<City> cities)
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
