using System;

namespace SanAndreasMail.Domain
{
    /// <summary>
    /// Route Between Cities 
    /// </summary>
    public class Route
    {
        public Guid RouteId { get; set; }
        public Guid OriginId { get; set; }
        public Guid DestinyId { get; set; }
        public int TotalTravelTime { get; set; }

        public virtual City Origin { get; set; }
        public virtual City Destiny { get; set; }

        public override string ToString()
        {
            return "Origin: " + this.Origin.Abbreviation + "; Destiny: " + this.Destiny.Abbreviation + "; Travel Time: " + this.TotalTravelTime;
        }
    }
}
