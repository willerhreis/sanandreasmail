using System;
using System.Collections.Generic;
using System.Text;

namespace SanAndreasMail.Domain.Models
{
    public class Order
    {
        public Guid OriginId { get; set; }
        public string Origin { get; set; }
        public Guid DestinyId { get; set; }
        public string Destiny { get; set; }
    }
}
