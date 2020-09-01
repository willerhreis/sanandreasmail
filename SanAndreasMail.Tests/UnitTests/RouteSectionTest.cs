using SanAndreasMail.Domain;
using SanAndreasMail.Domain.Services;
using SanAndreasMail.Infra.Helpers;
using SanAndreasMail.Tests.UnitTests.ServiceFake;
using System;
using System.Collections.Generic;
using Xunit;

namespace SanAndreasMail.Tests.UnitTests
{
    public class RouteSectionTest
    {
        public IRouteSectionService _routeSectionService { get; set; }

        public RouteSectionTest()
        {
            CityServiceFake cityServiceFake = new CityServiceFake();
            _routeSectionService = new RouteSectionServiceFake(cityServiceFake);
        }

        [Fact(DisplayName = "Carrega os trechos a partir do arquivo de entrada")]
        public async void GenerateByEntry_RouteSections_ResultListOfRouteSections()
        {
            //Act
            List<RouteSection> routeSections = await _routeSectionService.GetRouteSections(Utility.ReadFile("./UnitTests/Artefacts/trechos.txt"));

            //Assert
            Assert.NotNull(routeSections);
        }

        [Fact(DisplayName = "Lista de todas os trechos cadastrados")]
        public async void List_RouteSections_ResultAllObjects()
        {
            await _routeSectionService.GetRouteSections(Utility.ReadFile("./UnitTests/Artefacts/trechos.txt"));

            //Act
            IEnumerable<RouteSection> routeSections = await _routeSectionService.ListAsync();

            //Assert
            Assert.NotEmpty(routeSections);
        }

        [Fact(DisplayName = "Verifica retorno da exception no método")]
        public async void Should_Be_Generate_NotImplementedException()
        {
            RouteSection routeSection = new RouteSection
            {
                Destiny = Guid.NewGuid(),
                Origin = Guid.NewGuid(),
                TravelTime = 1,
                RouteSectionId = Guid.NewGuid()
            };

            await Assert.ThrowsAsync<NotImplementedException>(async () =>
             {
                 await _routeSectionService.UpdateAsync(routeSection.RouteSectionId, routeSection);
             });//check if method returns exception
        }

        [Fact(DisplayName = "Pesquisa trecho pelo identificador")]
        public async void FindById_RouteService_ResultObjectNotFound()
        {

            await _routeSectionService.GetRouteSections(Utility.ReadFile("./UnitTests/Artefacts/trechos.txt"));

            Guid guidTOSearch = Guid.NewGuid();

            //Act
            RouteSection routeSection = await _routeSectionService.FindByIdAsync(guidTOSearch);

            //Assert
            Assert.Null(routeSection);
        }
    }
}
