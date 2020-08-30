using System;
using System.Collections.Generic;
using System.Text;

namespace SanAndreasMail.Domain.Models
{
    public class Order
    {
        public Guid Origin { get; set; }
        public Guid Destiny { get; set; }
    }
}
