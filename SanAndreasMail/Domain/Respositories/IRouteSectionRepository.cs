using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanAndreasMail.Domain.Respositories
{
    public interface IRouteSectionRepository
    {
        Task<IEnumerable<RouteSection>> ListAsync();

        Task<RouteSection> AddAsync(RouteSection routeSection);

        Task<RouteSection> FindByIdAsync(Guid id);

        void Update(Guid id, RouteSection routeSection);
    }
}
