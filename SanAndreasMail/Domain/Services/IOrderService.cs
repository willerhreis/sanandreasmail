using SanAndreasMail.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SanAndreasMail.Domain.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrders(List<string> ordersInput);
    }
}
