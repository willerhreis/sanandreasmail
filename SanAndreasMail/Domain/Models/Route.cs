using System;

namespace SanAndreasMail.Domain
{
    /// <summary>
    /// Route Between Cities 
    /// </summary>
    public class Route
    {
        public Guid RouteId { get; set; }
        public Guid Origin { get; set; }
        public Guid Destiny { get; set; }
        public int TotalTravelTime { get; set; }

    }
}
