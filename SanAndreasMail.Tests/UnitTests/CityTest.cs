using SanAndreasMail.Domain;
using SanAndreasMail.Domain.Services;
using SanAndreasMail.Tests.UnitTests.ServiceFake;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SanAndreasMail.Tests.UnitTests
{
    public class CityTest
    {
        public ICityService _cityService { get; set; }

        public CityTest()
        {
            _cityService = new CityServiceFake();
        }

        [Fact(DisplayName = "Cadastra uma nova cidade")]
        public async void Add_City_ResultObjectAdded()
        {
            City newCity = new City
            {
                Abbreviation = "FC",
                Name = "Flint County"
            };

            //Act
            City cityAdded = await _cityService.SaveAsync(newCity);

            //Assert
            Assert.NotNull(cityAdded);
            Assert.Equal(newCity.Abbreviation, cityAdded.Abbreviation);
            Assert.Equal(newCity.Name, cityAdded.Name);
            Assert.NotEqual(newCity.CityId, Guid.Empty);
        }

        [Fact(DisplayName = "Lista de todas as cidades cadastradas")]
        public async void List_City_ResultAllObjects()
        {
            //Act
            IEnumerable<City> cities = await _cityService.ListAsync();

            //Assert
            Assert.NotEmpty(cities);
        }

        [Fact(DisplayName = "Pesquisa cidade pelo identificador")]
        public async void FindById_City_ResultObjectNotFound()
        {
            Guid guidTOSearch = Guid.NewGuid();

            //Act
            City city = await _cityService.FindByIdAsync(guidTOSearch);

            //Assert
            Assert.Null(city);
        }

        [Fact(DisplayName = "Pesquisa cidade pela sigla")]
        public async void FindByAbbreviation_City_ResultObjectFound()
        {
            string abbreviation = "LS";

            //Act
            City city = await _cityService.FindByAbbreviationAsync(abbreviation);

            //Assert
            Assert.NotNull(city);
        }

        [Fact(DisplayName = "Verifica retorno da exception no método")]
        public async void Should_Be_Generate_NotImplementedException()
        {
            City newCity = new City
            {
                CityId = Guid.NewGuid(),
                Abbreviation = "FC",
                Name = "Flint County"
            };

            await Assert.ThrowsAsync<NotImplementedException>(async () =>
            {
                await _cityService.UpdateAsync(newCity.CityId, newCity);
            });//check if method returns exception
        }
    }
}
