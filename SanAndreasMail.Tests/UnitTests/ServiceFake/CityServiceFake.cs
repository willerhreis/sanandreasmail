using SanAndreasMail.Domain;
using SanAndreasMail.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanAndreasMail.Tests.UnitTests.ServiceFake
{
    public class CityServiceFake : ICityService
    {
        public List<City> _cities { get; set; }

        public CityServiceFake()
        {
            _cities = new List<City>()
            {
                new City() { CityId = Guid.NewGuid(), Abbreviation = "LS", Name = "Los Santos" },
                new City() { CityId = Guid.NewGuid(), Abbreviation = "SF", Name = "San Fierro" },
                new City() { CityId = Guid.NewGuid(), Abbreviation = "LV", Name = "Las Venturas" },
                new City() { CityId = Guid.NewGuid(), Abbreviation = "RC", Name = "Red County" },
                new City() { CityId = Guid.NewGuid(), Abbreviation = "WS", Name = "Whetstone" },
                new City() { CityId = Guid.NewGuid(), Abbreviation = "BC", Name = "Bone County" }
            };
        }

        public async Task<IEnumerable<City>> ListAsync()
        {
            return await Task.Run(() => _cities);
        }

        public async Task<City> SaveAsync(City city)
        {
            city.CityId = Guid.NewGuid();
            await Task.Run(() => _cities.Add(city));
            return city;
        }

        public async Task<City> FindByIdAsync(Guid id)
        {
            return await Task.Run(() => _cities.Where(a => a.CityId == id).FirstOrDefault());
        }

        public async Task<City> FindByAbbreviationAsync(string abbreviation)
        {
            return await Task.Run(() => _cities.Where(a => a.Abbreviation == abbreviation).FirstOrDefault());
        }

        public Task<City> UpdateAsync(Guid id, City city)
        {
            throw new NotImplementedException();
        }
    }
}
