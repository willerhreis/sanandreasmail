using System;
using System.Collections.Generic;
using System.Text;

namespace SanAndreasMail.Domain.Interfaces
{
    interface IRoute
    {
        /// <summary>
        /// Get route (origin and destiny) and return the shortest route
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        int GetShortestRoute(Route route);
    }
}
