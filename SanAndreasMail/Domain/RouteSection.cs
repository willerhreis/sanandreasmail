using System;
using System.Collections.Generic;
using System.Text;

namespace SanAndreasMail.Domain
{
    /// <summary>
    /// Route Section Between Cities
    /// </summary>
    public class RouteSection
    {
        /// <summary>
        /// Identifier of route
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Route Origin
        /// </summary>
        public City Origin { get; set; }
        /// <summary>
        /// Route Destiny
        /// </summary>
        public City Destiny { get; set; }
        /// <summary>
        /// Travel time of Between origin and destiny
        /// </summary>
        public int TravelTime { get; set; }
    }
}
