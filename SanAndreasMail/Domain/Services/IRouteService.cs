using SanAndreasMail.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SanAndreasMail.Domain.Services
{
    public interface IRouteService
    {
        //List Routes
        Task<IEnumerable<Route>> ListAsync();
        //Create new route
        Task<Route> SaveAsync(Route route);

        //Find route by id
        Task<Route> FindByIdAsync(Guid id);

        //Update route
        Task<Route> UpdateAsync(Guid id, Route route);

        Task<List<Route>> GetShortestRoute(Order order);
    }
}
