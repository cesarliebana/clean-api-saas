using FluentAssertions;
using MediatR;
using SaaV.SaaS.Core.Domain.Requests;
using SaaV.SaaS.Core.Domain.Responses;
using SaaV.SaaS.Core.Shared.Exceptions;
using SaaV.SaaS.Infrastructure.Persistence;
using SaaV.SaaS.UnitTests.Factories;

namespace SaaV.SaaS.UnitTests.Tests
{
    public class DummyTest: SqlLiteTest
    {
        private readonly IMediator _mediator;

        public DummyTest(IMediator mediator, SaaSDbContext dbContext): base(dbContext)
        { 
            _mediator = mediator;
        }

        private async Task<GetDummyResponse> CreateDummy()
        {
            CreateDummyRequest createDummyRequest = DummyFactory.GetCreateDummyRequest();
            GetDummyResponse getDummyResponse = await _mediator.Send(createDummyRequest);
            return getDummyResponse;
        }

        private static void CheckGetDummyResponse(GetDummyResponse getDummyResponse, int id)
        {
            getDummyResponse.Id.Should().BeGreaterThan(0).And.Be(id);
            getDummyResponse.Name.Should().NotBeNullOrEmpty();
            getDummyResponse.ModifiedDateTime.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        }


        [Fact]
        public async Task CreateDummy_Success_Test()
        {
            GetDummyResponse getDummyResponse = await CreateDummy();

            CheckGetDummyResponse(getDummyResponse, getDummyResponse.Id);
        }

        [Fact]
        public async Task UpdateDummy_Success_Test()
        {
            GetDummyResponse getDummyResponse = await CreateDummy();
            UpdateDummyRequest updateDummyRequest = DummyFactory.GetUpdateDummyRequest(getDummyResponse);
            
            getDummyResponse = await _mediator.Send(updateDummyRequest);
            
            CheckGetDummyResponse(getDummyResponse, updateDummyRequest.Id);
        }



        [Fact]
        public async Task UpdateDummy_NotExists_Test()
        {
            GetDummyResponse getDummyResponse = await CreateDummy();
            UpdateDummyRequest updateDummyRequest = DummyFactory.GetUpdateDummyRequest(getDummyResponse);
            updateDummyRequest.Id = 0;
            
            Func<Task> action = async () => { await _mediator.Send(updateDummyRequest); };
            
            await action.Should().ThrowAsync<ItemNotFoundException>();
        }

        [Fact]
        public async Task GetAllDummies_Success_Test()
        {
            await CreateDummy();
            await CreateDummy();

            GetAllDummiesResponse getAllDummiesResponse = await _mediator.Send(new GetAllDummiesRequest());

            getAllDummiesResponse.Dummies.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task GetDummyById_Success_Test()
        {
            GetDummyResponse getCreateDummyResponse = await CreateDummy();
            GetDummyByIdRequest getDummyByIdRequest = new(getCreateDummyResponse.Id);

            GetDummyResponse getDummyByIdResponse = await _mediator.Send(getDummyByIdRequest);

            getDummyByIdResponse.Id.Should().Be(getCreateDummyResponse.Id);
            getDummyByIdResponse.Name.Should().Be(getCreateDummyResponse.Name);
            getDummyByIdResponse.ModifiedDateTime.Should().Be(getCreateDummyResponse.ModifiedDateTime);
        }

        [Fact]
        public async Task GetDummyById_NotExists_Test()
        {
            Func<Task> action = async () => { await _mediator.Send(new UpdateDummyRequest()); };

            await action.Should().ThrowAsync<ItemNotFoundException>();
        }

        [Fact]
        public async Task DeleteDummy_Success_Test()
        {
            GetDummyResponse getCreateDummyResponse = await CreateDummy();

            await _mediator.Send(new DeleteDummyRequest(getCreateDummyResponse.Id));
            Func<Task> action = async () => { await _mediator.Send(new GetDummyByIdRequest(getCreateDummyResponse.Id)); };

            await action.Should().ThrowAsync<ItemNotFoundException>();
        }


        [Fact]
        public async Task DeleteDummy_NotExists_Test()
        {
            Func<Task> action = async () => { await _mediator.Send(new DeleteDummyRequest()); };

            await action.Should().ThrowAsync<ItemNotFoundException>();
        }
    }
}