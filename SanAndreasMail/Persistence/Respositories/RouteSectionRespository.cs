using Microsoft.EntityFrameworkCore;
using SanAndreasMail.Domain;
using SanAndreasMail.Domain.Respositories;
using SanAndreasMail.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanAndreasMail.Persistence.Respositories
{

    public class RouteSectionRespository : BaseRepository, IRouteSectionRespository
    {
        public RouteSectionRespository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<RouteSection>> ListAsync()
        {
            return await _context.RouteSections.ToListAsync();
        }

        public async Task<RouteSection> AddAsync(RouteSection routeSection)
        {
            await _context.RouteSections.AddAsync(routeSection);
            return routeSection;
        }

        public async Task<RouteSection> FindByIdAsync(Guid id)
        {
            return await _context.RouteSections.Where(a => a.RouteSectionId == id).AsTracking().FirstOrDefaultAsync();
        }

        public void Update(Guid id, RouteSection routeSection)
        {
            _context.RouteSections.Update(routeSection);
        }

    }
}
