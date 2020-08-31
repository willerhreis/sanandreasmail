using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SanAndreasMail.Domain.Respositories
{
    public interface IRouteRepository
    {
        Task<IEnumerable<Route>> ListAsync();
        Task<Route> AddAsync(Route city);
        Task<Route> FindByIdAsync(Guid id);
        void Update(Guid id, Route route);
    }
}
