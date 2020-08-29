using SanAndreasMail.Domain;
using SanAndreasMail.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanAndreasMail.Infra
{
    public class CalculateRoute : ICalculateRoute
    {
        /// <summary>
        /// Get the shortest of route between two cities
        /// </summary>
        /// <param name="route"></param>
        public int GetShortestRoute(Route route)
        {

            //TODO: calculate routes and set travel time
            return route.TravelTime;
        }
    }
}
