using SanAndreasMail.Domain;
using SanAndreasMail.Domain.Models;
using SanAndreasMail.Domain.Services;
using SanAndreasMail.Infra.Helpers;
using SanAndreasMail.Tests.UnitTests.ServiceFake;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SanAndreasMail.Tests.UnitTests
{
    public class RouteTest
    {
        public IRouteService _routeServiceFake { get; set; }
        public IRouteSectionService _routeSectionServiceFake { get; set; }
        public ICityService _cityServiceFake { get; set; }

        public RouteTest()
        {
            CityServiceFake cityServiceFake = new CityServiceFake();
            _cityServiceFake = cityServiceFake;

            RouteSectionServiceFake routeSection = new RouteSectionServiceFake(cityServiceFake);
            _routeSectionServiceFake = routeSection;

            _routeServiceFake = new RouteServiceFake(routeSection, cityServiceFake);
        }

        [Fact(DisplayName = "Captura o menor caminho a partir da encomenda")]        
        public async void Get_ShortestPath_By_Order_ResultPath()
        {
            await _routeSectionServiceFake.GetRouteSections(Utility.ReadFile("./UnitTests/Artefacts/trechos.txt"));


            IEnumerable<City> cities = await _cityServiceFake.ListAsync();

            City origin = cities.Where(a => a.Abbreviation == "LS").FirstOrDefault();
            City destiny = cities.Where(a => a.Abbreviation == "BC").FirstOrDefault();
            City cityLV = cities.Where(a => a.Abbreviation == "LV").FirstOrDefault();


            Order order = new Order
            {
                Destiny = destiny,
                Origin = origin,
            };

            //Act
            List<Route> routePaths = await _routeServiceFake.GetShortestRoute(order);

            //Should be this:

            Route route_1 = new Route
            {
                Destiny = cityLV,
                Origin = origin,
                DestinyId = cityLV.CityId,
                OriginId = origin.CityId,
                TotalTravelTime = 1,
                RouteId = Guid.Empty
            };

            Route route_2 = new Route
            {
                Destiny = destiny,
                Origin = cityLV,
                DestinyId = destiny.CityId,
                OriginId = cityLV.CityId,
                TotalTravelTime = 1,
                RouteId = Guid.Empty
            };

            List<Route> routePathsReturn = new List<Route>
            {
                route_1, route_2
            };

            //Assert
            Assert.NotNull(routePaths);
            Assert.Empty(routePaths);

            //EXPECTED: LS - LV - BC
            Assert.Equal(routePathsReturn, routePaths);
        }
        
        [Fact(DisplayName = "Verifica retorno da exception no método")]
        public async void Should_Be_Generate_NotImplementedException()
        {

            City origin = await _cityServiceFake.FindByAbbreviationAsync("WS");
            City destiny = await _cityServiceFake.FindByAbbreviationAsync("BC");

            Route route = new Route
            {
                Destiny = destiny,
                Origin = origin,
                RouteId = Guid.NewGuid()
            };

            await Assert.ThrowsAsync<NotImplementedException>(async () =>
             {
                 await _routeServiceFake.UpdateAsync(route.RouteId, route);
             });//check if method returns exception
        }

       
    }
}
