using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SanAndreasMail.Domain.Services
{
    public interface ICityService
    {
        //List Cities
        Task<IEnumerable<City>> ListAsync();
        //Create new city
        Task<City> SaveAsync(City city);

        //Find city by id
        Task<City> FindByIdAsync(Guid id);

        Task<City> FindByAbbreviationAsync(string abbreviation);

        //Update city
        Task<City> UpdateAsync(Guid id, City city);
    }
}
