using SanAndreasMail.Domain;
using SanAndreasMail.Domain.Respositories;
using SanAndreasMail.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SanAndreasMail.Services
{
    public class RouteService  : IRouteService
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RouteService(IUnitOfWork unitOfWork,
            IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
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
        public int GetShortestRoute(Route route)
        {

            if (route == null)
                throw new Exception("Invalid arguments.");

            //TODO: calculate routes and set travel time
            return route.TotalTravelTime;
        }
    }
}
