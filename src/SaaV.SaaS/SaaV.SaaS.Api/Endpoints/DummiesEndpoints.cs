using MediatR;
using SaaV.SaaS.Core.Domain.Requests;
using SaaV.SaaS.Core.Domain.Responses;
using SaaV.SaaS.WebApi.Models;

namespace SaaV.SaaS.WebApi.Endpoints
{
    internal static class DummiesEndpoints
    {
        public static async Task<IResult> GetAllDummies(IMediator mediator)
        {
            GetAllDummiesRequest getAllDummiesRequest = new();
            GetAllDummiesResponse getAllDummiesResponse = await mediator.Send(getAllDummiesRequest);
            
            return Results.Ok(getAllDummiesResponse);
        }

        public static async Task<IResult> GetDummyById(int id, IMediator mediator)
        {
            GetDummyByIdRequest getDummyByIdRequest = new(id);
            GetDummyResponse getDummyByIdResponse = await mediator.Send(getDummyByIdRequest);
            
            return Results.Ok(getDummyByIdResponse);
        }

        public async static Task<IResult> CreateDummy(CreateDummyModel createDummyModel, IMediator mediator)
        {
            CreateDummyRequest createDummyRequest = new(createDummyModel.Name);
            GetDummyResponse createDummyResponse = await mediator.Send(createDummyRequest);
            
            return Results.Created($"/dummies/{createDummyResponse.Id}", createDummyResponse);
        }

        public static async Task<IResult> UpdateDummy(int id, UpdateDummyModel updateDummyModel, IMediator mediator)
        {
            UpdateDummyRequest updateDummyRequest = new(id, updateDummyModel.Name);
            GetDummyResponse updateDummyResponse = await mediator.Send(updateDummyRequest);
                       
            return Results.Ok(updateDummyResponse);
        }

        public static async Task<IResult> DeleteDummy(int id, IMediator mediator)
        {
            DeleteDummyRequest deleteDummyRequest = new(id);
            await mediator.Send(deleteDummyRequest);
            
            return Results.NoContent();
        }
    }
}
