using SanAndreasMail.Domain;
using SanAndreasMail.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanAndreasMail.Tests.UnitTests.ServiceFake
{
    public class RouteSectionServiceFake : IRouteSectionService
    {
        public List<RouteSection> _routeSections { get; set; }
        private CityServiceFake _cityServiceFake;

        public RouteSectionServiceFake(CityServiceFake cityService)
        {
            _cityServiceFake = cityService;
        }

        public async Task<RouteSection> FindByIdAsync(Guid id)
        {
            return await Task.Run(() => _routeSections.Where(a => a.RouteSectionId == id).FirstOrDefault());
        }

        public async Task<List<RouteSection>> GetRouteSections(List<string> routeSectionsText)
        {
            _routeSections = new List<RouteSection>();

            foreach (string routeSection in routeSectionsText)
            {
                string[] section = routeSection.Split(' ');

                string abbreviationOrigin = section[0];
                string abbreviationDestiny = section[1];
                int travelTime = Convert.ToInt32(section[2]);

                City origin = await _cityServiceFake.FindByAbbreviationAsync(abbreviationOrigin);
                City destiny = await _cityServiceFake.FindByAbbreviationAsync(abbreviationDestiny);

                RouteSection routeSectionObj = new RouteSection
                {
                    RouteSectionId = Guid.NewGuid(),
                    Origin = origin.CityId,
                    Destiny = destiny.CityId,
                    TravelTime = travelTime
                };

                //Add to list to return
                _routeSections.Add(routeSectionObj);
            }

            return _routeSections;
        }

        public async Task<IEnumerable<RouteSection>> ListAsync()
        {
            return await Task.Run(() => _routeSections);
        }

        public async Task<RouteSection> SaveAsync(RouteSection route)
        {
            route.RouteSectionId = Guid.NewGuid();
            await Task.Run(() => _routeSections.Add(route));
            return route;
        }

        public Task<RouteSection> UpdateAsync(Guid id, RouteSection route)
        {
            throw new NotImplementedException();
        }
    }
}
