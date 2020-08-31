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
    public class RouteRepository : BaseRepository, IRouteRepository
    {
        public RouteRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Route>> ListAsync()
        {
            return await _context.Routes.ToListAsync();
        }

        public async Task<Route> AddAsync(Route city)
        {
            await _context.Routes.AddAsync(city);
            return city;
        }

        public async Task<Route> FindByIdAsync(Guid id)
        {
            return await _context.Routes.Where(a => a.RouteId == id).AsTracking().FirstOrDefaultAsync();
        }

        public void Update(Guid id, Route route)
        {
            _context.Routes.Update(route);
        }

    }
}
