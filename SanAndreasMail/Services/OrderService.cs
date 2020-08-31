using SanAndreasMail.Domain;
using SanAndreasMail.Domain.Models;
using SanAndreasMail.Domain.Respositories;
using SanAndreasMail.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SanAndreasMail.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICityRepository _cityRepository;

        public OrderService() { }

        public OrderService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<List<Order>> GetOrders(List<string> ordersInput)
        {
            List<Order> orders = new List<Order>();

            Console.WriteLine("\nEncomendas: \n");

            foreach (string order in ordersInput)
            {
                string[] orderSplit = order.Split(' ');

                string orderOrigin = orderSplit[0];
                string orderDestiny = orderSplit[1];

                City origin = await _cityRepository.FindByAbbreviationAsync(orderOrigin);
                City destiny = await _cityRepository.FindByAbbreviationAsync(orderDestiny);

                Order orderObj = new Order
                {
                    Origin = origin,
                    Destiny = destiny,
                };

                Console.WriteLine(" - " + origin.Name + " ----> " + destiny.Name);

                orders.Add(orderObj);
            }

            return orders;
        }
    }
}
