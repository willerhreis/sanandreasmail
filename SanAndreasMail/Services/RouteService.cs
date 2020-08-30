using SanAndreasMail.Domain;
using SanAndreasMail.Domain.Models;
using SanAndreasMail.Domain.Respositories;
using SanAndreasMail.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanAndreasMail.Services
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _routeRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IRouteSectionRepository _routeSectionsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RouteService(IUnitOfWork unitOfWork,
            IRouteRepository routeRepository, IRouteSectionRepository routeSectionsRepository, ICityRepository cityRepository)
        {
            _routeRepository = routeRepository;
            _routeSectionsRepository = routeSectionsRepository;
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<Route> FindByIdAsync(Guid id)
        {
            return await _routeRepository.FindByIdAsync(id);
        }

        public async Task<IEnumerable<Route>> ListAsync()
        {
            return await _routeRepository.ListAsync();
        }

        public async Task<Route> SaveAsync(Route route)
        {
            try
            {
                await _routeRepository.AddAsync(route);
                await _unitOfWork.CompleteAsync();

                return route;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred when creating the route: {ex.Message}");
            }
        }

        public Task<Route> UpdateAsync(Guid id, Route route)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the shortest of route between two cities
        /// </summary>
        /// <param name="route"></param>
        public async Task<List<Route>> GetShortestRoute(Order order)
        {
            if (order == null)
                throw new Exception("Invalid arguments.");

            return null;
        }
    }
}
