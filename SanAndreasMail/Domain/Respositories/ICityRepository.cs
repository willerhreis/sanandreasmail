using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanAndreasMail.Domain.Respositories
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> ListAsync();

        Task<City> AddAsync(City city);

        Task<City> FindByIdAsync(Guid id);

        Task<City> FindByAbbreviationAsync(string abbreviation);

        void Update(Guid id, City city);
       
    }
}
