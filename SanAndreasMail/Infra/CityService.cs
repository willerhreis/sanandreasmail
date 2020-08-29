using SanAndreasMail.Domain;
using SanAndreasMail.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanAndreasMail.Infra
{
    public class CityService : ICity
    {
        public City CreateNewCity(City city)
        {
            if (city == null)
                throw new Exception("Invalid arguments.");

            city.Id = new Guid();
            return city;
        }

    }
}
