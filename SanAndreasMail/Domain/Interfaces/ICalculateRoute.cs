using System;
using System.Collections.Generic;
using System.Text;

namespace SanAndreasMail.Domain.Interfaces
{
    interface ICalculateRoute
    {
        /// <summary>
        /// Get route (origin and destiny) and return the shortest route
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        List<Route> GetShortestRoute(Route route);
    }
}
