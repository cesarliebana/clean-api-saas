using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SaaV.SaaS.Api.Extensions;
using SaaV.SaaS.Api.Validators;
using SaaV.SaaS.Core.Domain.Handlers;
using SaaV.SaaS.Infrastructure.Persistence;
using SaaV.SaaS.WebApi.Extensions;
using SaaV.SaaS.WebApi.Middlewares;

namespace SaaV.SaaS.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<SaaSDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SaaSConnectionString")));

            // Add services to the container.           
            builder.Services.AddJwtAuthentication();
            builder.Services.AddAuthorization();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<CreateDummyHandler>());
            builder.Services.AddValidatorsFromAssemblyContaining<CreateDummyValidator>();

            builder.Services.AddProviders();
            builder.Services.AddRepositories();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();          

            app.MapDummyEndpoints();
            app.MapAuthenticationEndpoints();

            app.UseMiddleware<ExceptionMiddleware>();

            app.Run();
        }
    }
}