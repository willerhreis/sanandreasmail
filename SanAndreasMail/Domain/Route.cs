using System;

namespace SanAndreasMail.Domain
{
    /// <summary>
    /// Route Between Cities 
    /// </summary>
    public class Route
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
        /// Total Travel time of route
        /// </summary>
        public int TotalTravelTime { get; set; }

    }
}
