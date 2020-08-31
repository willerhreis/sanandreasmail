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
            return "| Origem: " + this.Origin.Abbreviation + " | Destino: " + this.Destiny.Abbreviation + " | Tempo de Percurso: " + this.TotalTravelTime + " dia(s) |";
        }
    }
}
