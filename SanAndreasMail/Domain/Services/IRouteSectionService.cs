using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SanAndreasMail.Domain.Services
{
    public interface IRouteSectionService
    {
        //List Route sections
        Task<IEnumerable<RouteSection>> ListAsync();

        //Create new route section
        Task<RouteSection> SaveAsync(RouteSection route);

        //Find route section by id
        Task<RouteSection> FindByIdAsync(Guid id);

        //Update route section
        Task<RouteSection> UpdateAsync(Guid id, RouteSection route);
    }
}
