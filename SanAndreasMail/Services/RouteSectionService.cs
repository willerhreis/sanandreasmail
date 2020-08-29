using SanAndreasMail.Domain;
using SanAndreasMail.Domain.Respositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanAndreasMail.Services
{
    public class RouteSectionService
    {
        private readonly IRouteSectionRepository _routeSectionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RouteSectionService(IUnitOfWork unitOfWork,
            IRouteSectionRepository rouSectionteRepository)
        {
            _routeSectionRepository = rouSectionteRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<RouteSection> FindByIdAsync(Guid id)
        {
            return await _routeSectionRepository.FindByIdAsync(id);
        }

        public async Task<IEnumerable<RouteSection>> ListAsync()
        {
            return await _routeSectionRepository.ListAsync();
        }

        public async Task<RouteSection> SaveAsync(RouteSection routeSection)
        {
            try
            {
                await _routeSectionRepository.AddAsync(routeSection);
                await _unitOfWork.CompleteAsync();

                return routeSection;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred when creating the route section: {ex.Message}");
            }
        }

        public Task<RouteSection> UpdateAsync(Guid id, RouteSection routeSection)
        {
            throw new NotImplementedException();
        }
    }
}
