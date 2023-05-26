using MediatR;
using SaaV.SaaS.Core.Domain.Requests;
using SaaV.SaaS.Core.Domain.Responses;
using SaaV.SaaS.WebApi.Models;

namespace SaaV.SaaS.WebApi.Endpoints
{
    internal static class DummiesEndpoints
    {
        public static IResult GetAllDummies(IMediator mediator)
        {
            GetAllDummiesRequest getAllDummiesRequest = new();
            GetAllDummiesResponse getAllDummiesResponse = mediator.Send(getAllDummiesRequest).Result;
            
            return Results.Ok(getAllDummiesResponse);
        }

        public static IResult GetDummyById(int id, IMediator mediator)
        {
            GetDummyByIdRequest getDummyByIdRequest = new(id);
            GetDummyResponse getDummyByIdResponse = mediator.Send(getDummyByIdRequest).Result;
            
            return Results.Ok(getDummyByIdResponse);
        }

        public static IResult CreateDummy(CreateDummyModel createDummyModel, IMediator mediator)
        {
            CreateDummyRequest createDummyRequest = new(createDummyModel.Name);
            GetDummyResponse createDummyResponse = mediator.Send(createDummyRequest).Result;
            
            return Results.Created($"/dummies/{createDummyResponse.Id}", createDummyResponse);
        }

        public static IResult UpdateDummy(int id, UpdateDummyModel updateDummyModel, IMediator mediator)
        {
            UpdateDummyRequest updateDummyRequest = new(id, updateDummyModel.Name);
            GetDummyResponse updateDummyResponse = mediator.Send(updateDummyRequest).Result;
                       
            return Results.Ok(updateDummyResponse);
        }

        public static IResult DeleteDummy(int id, IMediator mediator)
        {
            DeleteDummyRequest deleteDummyRequest = new(id);
            mediator.Send(deleteDummyRequest);
            
            return Results.NoContent();
        }
    }
}
