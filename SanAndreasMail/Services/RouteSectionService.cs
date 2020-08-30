using SanAndreasMail.Domain;
using SanAndreasMail.Domain.Respositories;
using SanAndreasMail.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanAndreasMail.Services
{
    public class RouteSectionService : IRouteSectionService
    {
        private readonly IRouteSectionRepository _routeSectionRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RouteSectionService(IUnitOfWork unitOfWork,
            IRouteSectionRepository rouSectionteRepository, ICityRepository cityRepository)
        {
            _routeSectionRepository = rouSectionteRepository;
            _cityRepository = cityRepository;
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

        public async Task<List<RouteSection>> GetRouteSections(List<string> routeSectionsText)
        {
            List<RouteSection> routeSections = new List<RouteSection>();

            Console.WriteLine("\nTrechos: \n");

            foreach (string routeSection in routeSectionsText)
            {
                string[] section = routeSection.Split(' ');

                string abbreviationOrigin = section[0];
                string abbreviationDestiny = section[1];
                int travelTime = Convert.ToInt32(section[2]);

                City origin = await _cityRepository.FindByAbbreviationAsync(abbreviationOrigin);
                City destiny = await _cityRepository.FindByAbbreviationAsync(abbreviationDestiny);

                RouteSection routeSectionObj = new RouteSection
                {
                    RouteSectionId = Guid.NewGuid(),
                    Origin = origin.CityId,
                    Destiny = destiny.CityId,
                    TravelTime = travelTime
                };

                //Add to database
                await _routeSectionRepository.AddAsync(routeSectionObj);
                await _unitOfWork.CompleteAsync();

                Console.WriteLine(origin.Name + " -- to --> " + destiny.Name + " ( " + routeSectionObj.TravelTime + " dia(s) ) ");

                //Add to list to return
                routeSections.Add(routeSectionObj);
            }

            return routeSections;
        }
    }
}
