using System;
using System.Collections.Generic;
using System.Text;

namespace SanAndreasMail.Domain.Models
{
    public class Order
    {
        public City Origin { get; set; }
        public City Destiny { get; set; }
    }
}
