using Microsoft.VisualStudio.TestTools.UnitTesting;
using SanAndreasMail.Domain;
using SanAndreasMail.Domain.Models;
using SanAndreasMail.Services;
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

            Order order = new Order
            {
                Origin = city_sf,
                Destiny = city_ws,
            };

            //System under test
            var sut = new OrderService();

            /*var resultOfCalc = sut.GetShortestRoute(order);

            Assert.AreEqual(route.TotalTravelTime, resultOfCalc);*/

        }
    }
}
