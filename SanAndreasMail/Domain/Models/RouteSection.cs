﻿using System;

namespace SanAndreasMail.Domain
{
    /// <summary>
    /// Route Section Between Cities
    /// </summary>
    public class RouteSection
    {
        public Guid RouteSectionId { get; set; }
        public Guid Origin { get; set; }
        public Guid Destiny { get; set; }
        public int TravelTime { get; set; }
    }
}
