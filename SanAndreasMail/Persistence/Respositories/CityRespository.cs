using Microsoft.EntityFrameworkCore;
using SanAndreasMail.Domain;
using SanAndreasMail.Domain.Respositories;
using SanAndreasMail.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanAndreasMail.Persistence.Respositories
{
    public class CityRespository : BaseRepository, ICityRepository
    {
        public CityRespository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<City>> ListAsync()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<City> AddAsync(City city)
        {
            await _context.Cities.AddAsync(city);
            return city;
        }

        public async Task<City> FindByIdAsync(Guid id)
        {
            return await _context.Cities.Where(a => a.CityId == id).AsTracking().FirstOrDefaultAsync();
        }

        public void Update(Guid id, City city)
        {
            _context.Cities.Update(city);
        }

    }
}
