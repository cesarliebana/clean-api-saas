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
                .RequireAuthorization()
                .WithName("GetAllDummies")
                .WithSummary("Gets all dummies")
                .Produces<GetAllDummiesResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized)
                .WithOpenApi();

            builder.MapGet("/{id}", DummiesEndpoints.GetDummyById)
                .RequireAuthorization()
                .WithName("GetDummyById")
                .WithSummary("Gets a dummy with an id")
                .Produces<GetDummyResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status401Unauthorized)
                .WithOpenApi();

            builder.MapPost("/", DummiesEndpoints.CreateDummy)
                .RequireAuthorization()
                .WithName("CreateDummy")
                .WithSummary("Creates a dummy")
                .Accepts<CreateDummyModel>("application/json")
                .Produces<GetDummyResponse>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status401Unauthorized)
                .WithOpenApi();

            builder.MapPut("/{id}", DummiesEndpoints.UpdateDummy)
                .RequireAuthorization()
                .WithName("UpdateDummy")
                .WithSummary("Updates a dummy")
                .Accepts<UpdateDummyModel>("application/json")
                .Produces<GetDummyResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status401Unauthorized)
                .WithOpenApi();

            builder.MapDelete("/{id}", DummiesEndpoints.DeleteDummy)
                .RequireAuthorization()
                .WithName("DeleteDummy")
                .WithSummary("Deletes a dummy")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status401Unauthorized)
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