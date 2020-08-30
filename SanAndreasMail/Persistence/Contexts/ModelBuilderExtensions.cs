using Microsoft.EntityFrameworkCore;
using SanAndreasMail.Domain;
using System;

namespace SanAndreasMail.Persistence.Contexts
{
    public static class ModelBuilderExtensions 
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<City>().HasData(
                new City { CityId = Guid.NewGuid(), Abbreviation = "LS", Name = "Los Santos" },
                new City { CityId = Guid.NewGuid(), Abbreviation = "SF", Name = "San Fierro" },
                new City { CityId = Guid.NewGuid(), Abbreviation = "LV", Name = "Las Venturas" },
                new City { CityId = Guid.NewGuid(), Abbreviation = "RC", Name = "Red County" },
                new City { CityId = Guid.NewGuid(), Abbreviation = "WS", Name = "Whetstone" },
                new City { CityId = Guid.NewGuid(), Abbreviation = "BC", Name = "Bone County" }
            );
        }
    }
}
