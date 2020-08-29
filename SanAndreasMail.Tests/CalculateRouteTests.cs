using Microsoft.VisualStudio.TestTools.UnitTesting;
using SanAndreasMail.Domain;
using SanAndreasMail.Infra;
using System;

namespace SanAndreasMail.Tests
{
    [TestClass]
    public class CalculateRouteTests
    {
        [TestMethod]
        //Calculate route between two cities
        public void ShouldCalculateRoute()
        {
            City city_sf = new City
            {
                CityId = Guid.NewGuid(),
                Abbreviation = "SF",
                Name = "San Fierro"
            };

            City city_ws = new City
            {
                CityId = Guid.NewGuid(),
                Abbreviation = "WS",
                Name = " Whetstone "
            };

            Route route = new Route
            {
                Origin = city_sf.CityId,
                Destiny = city_ws.CityId,
                RouteId = Guid.NewGuid(),
                //Only for test
                TotalTravelTime = 1
            };

            //System under test
            var sut = new RouteService();

            var resultOfCalc = sut.GetShortestRoute(route);

            Assert.AreEqual(route.TotalTravelTime, resultOfCalc);

        }
    }
}
