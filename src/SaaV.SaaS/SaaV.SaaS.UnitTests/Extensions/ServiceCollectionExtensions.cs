using Microsoft.Extensions.DependencyInjection;
using SaaV.SaaS.Core.Domain;
using SaaV.SaaS.Core.Shared.Interfaces;
using SaaV.SaaS.Infrastructure.Persistence;
using SaaV.SaaS.Infrastructure.Repositories;
using SaaV.SaaS.UnitTest.Providers;

namespace SaaV.SaaS.UnitTests.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddProviders(this IServiceCollection services)
        {
            services.AddTransient<ICredentialProvider, CredentialProvider>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IDummyRepository, DummyRepository>();

        }
    }
}
