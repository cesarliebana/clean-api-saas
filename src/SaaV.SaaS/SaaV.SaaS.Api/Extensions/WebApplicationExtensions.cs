using SaaV.SaaS.Core.Domain.Responses;
using SaaV.SaaS.WebApi.Endpoints;
using SaaV.SaaS.WebApi.Models;

namespace SaaV.SaaS.WebApi.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void MapDummyEndpoints(this WebApplication app)
        {
            RouteGroupBuilder builder = app.MapGroup("/dummies").WithDisplayName("Dummies").WithOpenApi();
            builder.MapGet("/", DummiesEndpoints.GetAllDummies)
                .WithName("GetAllDummies")
                .WithDescription("Gets all dummies")
                .Produces<GetAllDummiesResponse>(StatusCodes.Status200OK)
                .WithOpenApi();

            builder.MapGet("/{id}", DummiesEndpoints.GetDummyById)
                .WithName("GetDummyById")
                .WithDescription("Gets a dummy with an id")
                .Produces<GetDummyResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithOpenApi();

            builder.MapPost("/", DummiesEndpoints.CreateDummy)
                .WithName("CreateDummy")
                .WithDescription("Creates a dummy")
                .Accepts<CreateDummyModel>("application/json")
                .Produces<GetDummyResponse>(StatusCodes.Status201Created)
                .WithOpenApi();

            builder.MapPut("/{id}", DummiesEndpoints.UpdateDummy)
                .WithName("UpdateDummy")
                .WithDescription("Updates a dummy")
                .Accepts<UpdateDummyModel>("application/json")
                .Produces<GetDummyResponse>(StatusCodes.Status200OK)
                .WithOpenApi();

            builder.MapDelete("/{id}", DummiesEndpoints.DeleteDummy)
                .WithName("DeleteDummy")
                .WithDescription("Deletes a dummy")
                .Produces(StatusCodes.Status204NoContent)
                .WithOpenApi();
        }

        public static void MapAuthenticationEndpoints(this WebApplication app)
        {
            RouteGroupBuilder builder = app.MapGroup("/auth").WithDisplayName("Authentication").WithOpenApi();
            builder.MapGet("/", AuthenticationEndpoints.GetBearerToken)
                .WithName("GetBearerToken")
                .WithSummary("Get a bearer token")
                .Produces<GetBearerTokenResponse>(StatusCodes.Status200OK)
                .WithOpenApi();

        }
    }
}