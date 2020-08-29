using SanAndreasMail.Domain;
using SanAndreasMail.Domain.Respositories;
using SanAndreasMail.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SanAndreasMail.Infra
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CityService(IUnitOfWork unitOfWork,
            ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
        }
   

        public Task<City> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<City>> ListAsync()
        {
            return await _cityRepository.ListAsync();
        }

        public async Task<City> SaveAsync(City city) 
        {
            try
            {
                await _cityRepository.AddAsync(city);
                await _unitOfWork.CompleteAsync();

                return city;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred when creating the city: {ex.Message}");
            }
        }

        public Task<City> UpdateAsync(Guid id, City city)
        {
            throw new NotImplementedException();
        }
    }
}
