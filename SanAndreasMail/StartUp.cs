using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SanAndreasMail.Domain.Respositories;
using SanAndreasMail.Domain.Services;
using SanAndreasMail.Infra.Helpers;
using SanAndreasMail.Persistence.Contexts;
using SanAndreasMail.Persistence.Respositories;
using SanAndreasMail.Services;

namespace SanAndreasMail
{
    public class StartUp
    {
        public ServiceProvider serviceProvider;

        public ServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(Utility.GetConnectionString("ConnectionStrings:DefaultConnection"));
            });

            services.AddMemoryCache();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddScoped<IRouteSectionRepository, RouteSectionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICityService, CityService>();

            serviceProvider = services.BuildServiceProvider();

            return services;
        }
    }
}
