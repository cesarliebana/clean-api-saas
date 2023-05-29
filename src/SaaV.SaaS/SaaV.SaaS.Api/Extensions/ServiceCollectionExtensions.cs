using AutoMapper;
using SaaV.SaaS.Core.Domain;
using SaaV.SaaS.Core.Shared.Interfaces;
using SaaV.SaaS.Infrastructure.Persistence;
using SaaV.SaaS.Infrastructure.Providers;
using SaaV.SaaS.Infrastructure.Repositories;

namespace SaaV.SaaS.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddProviders(this IServiceCollection services)
        {
            services.AddTransient<ITenantProvider, TenantProvider>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IDummyRepository, DummyRepository>();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Core.Shared.Mappings.MappingProfile());
            });
            services.AddSingleton(mappingConfig.CreateMapper());
        }
    }
}
