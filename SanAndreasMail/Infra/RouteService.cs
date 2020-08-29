using SanAndreasMail.Domain;
using SanAndreasMail.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanAndreasMail.Infra
{
    public class RouteService : IRoute
    {
        /// <summary>
        /// Get the shortest of route between two cities
        /// </summary>
        /// <param name="route"></param>
        public int GetShortestRoute(Route route)
        {

            if (route == null)
                throw new Exception("Invalid arguments.");

            //TODO: calculate routes and set travel time
            return route.TotalTravelTime;
        }
    }
}
