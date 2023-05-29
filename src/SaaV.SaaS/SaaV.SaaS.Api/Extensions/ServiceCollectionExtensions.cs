using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SaaV.SaaS.Core.Domain;
using SaaV.SaaS.Core.Shared.Interfaces;
using SaaV.SaaS.Infrastructure.Persistence;
using SaaV.SaaS.Infrastructure.Providers;
using SaaV.SaaS.Infrastructure.Repositories;
using SaaV.SaaS.WebApi.Endpoints;
using System.Text;

namespace SaaV.SaaS.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthenticationEndpoints.SecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

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
