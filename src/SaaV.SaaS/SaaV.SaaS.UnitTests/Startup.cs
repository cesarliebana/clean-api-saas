using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SaaV.SaaS.Core.Domain.Handlers;
using SaaV.SaaS.Infrastructure.Persistence;
using SaaV.SaaS.UnitTests.Extensions;

namespace SaaV.SaaS.UnitTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SaaSDbContext>(options => options.UseSqlite("DataSource=:memory:"));
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(CreateDummyHandler).Assembly));
            services.AddRepositories();
            services.AddProviders();
        }

    }
}
