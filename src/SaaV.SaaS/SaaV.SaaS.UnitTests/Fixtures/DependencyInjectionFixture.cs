using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SaaV.SaaS.Core.Domain.Handlers;
using SaaV.SaaS.Infrastructure.Persistence;
using SaaV.SaaS.UnitTests.Extensions;

namespace SaaV.SaaS.UnitTests.Fixtures
{
    public class DependencyInjectionFixture : IDisposable
    {
        public ServiceProvider ServiceProvider { get; private set; }

        public DependencyInjectionFixture()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<SaaSDbContext>(options => options.UseSqlite("DataSource=:memory:"));
            serviceCollection.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(CreateDummyHandler).Assembly));

            serviceCollection.AddProviders();
            serviceCollection.AddRepositories();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public void Dispose()
        {
            ServiceProvider.Dispose();
        }

        public void CheckSqlLiteDatabase()
        {
            using IServiceScope scope = ServiceProvider.CreateScope();
            SaaSDbContext dbContext = scope.ServiceProvider.GetRequiredService<SaaSDbContext>();
            dbContext.Database.OpenConnection();
            dbContext.Database.EnsureCreated();
        }
    }
}
