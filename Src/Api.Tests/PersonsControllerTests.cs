using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Friss.FraudDetection.Api.Controllers;
using Friss.FraudDetection.Api.Models;
using Friss.FraudDetection.Api.Models.Responses;
using Friss.FraudDetection.Contracts;
using Friss.FraudDetection.Contracts.Models;
using Friss.FraudDetection.Main.Contracts;
using Friss.FraudDetection.Main.Person;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Friss.FraudDetection.Api.Tests
{
    public class PersonsControllerTests
    {
        [Fact]
        public async Task GetPersonById_Ok()
        {
            // arrange
            var mockPersonMatchRuleService = new Mock<IRuleService<PersonModel>>();
            var mockPersonService = new Mock<IPersonService>();
            var personModel = new Mock<PersonModel>().Object;
            var id = 1;

            mockPersonService.Setup(service => service.GetPersonByIdAsync(id)).Returns(Task.FromResult(personModel));

            var controller = new PersonsController(mockPersonMatchRuleService.Object, mockPersonService.Object);

            var response = await controller.Get(1);

            response.Should().NotBeNull("The result should not be null.");
            var result = response.Result.As<OkObjectResult>().Value.As<ApiOkResponse<PersonModel>>();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Data.PersonId.Should().Be(personModel.PersonId, "The person id should match.");
        }

        [Fact]
        public async Task GetPersonById_NotFound()
        {
            // arrange
            var mockPersonMatchRuleService = new Mock<IRuleService<PersonModel>>();
            var mockPersonService = new Mock<IPersonService>();
            var personModel = new Mock<PersonModel>().Object;
            var id = 1;

            mockPersonService.Setup(service => service.GetPersonByIdAsync(id)).Returns(Task.FromResult<PersonModel>(null));

            var controller = new PersonsController(mockPersonMatchRuleService.Object, mockPersonService.Object);

            // act
            var response = await controller.Get(1);

            // assert
            response.Should().NotBeNull("The result should not be null.");
            var result = response.Result.As<NotFoundObjectResult>().Value.As<ApiResponse>();
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreatePerson_Ok()
        {
            // arrange
            var mockPersonMatchRuleService = new Mock<IRuleService<PersonModel>>();
            var mockPersonService = new Mock<IPersonService>();
            var personModel = new Mock<PersonModel>().Object;
            var id = 1;

            mockPersonService.Setup(service => service.CreatePersonAsync(personModel)).Returns(Task.FromResult(personModel));

            var controller = new PersonsController(mockPersonMatchRuleService.Object, mockPersonService.Object);

            // act
            var response = await controller.Create(personModel);

            //assert
            response.Should().NotBeNull("The result should not be null.");
            var result = response.Result.As<OkObjectResult>().Value.As<ApiOkResponse<PersonModel>>();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Data.PersonId.Should().Be(personModel.PersonId, "The person id should match.");
        }

        [Fact]
        public async Task CreatePerson_NotOk()
        {
            // arrange
            var mockPersonMatchRuleService = new Mock<IRuleService<PersonModel>>();
            var mockPersonService = new Mock<IPersonService>();
            var controller = new PersonsController(mockPersonMatchRuleService.Object, mockPersonService.Object);

            // act and assert
            var task = Assert.ThrowsAsync<ArgumentNullException>(async () => await controller.Create(null));
        }

        [Fact]
        public async Task CalculatePersonFraudProbability_Ok()
        {
            // arrange
            var mockPersonMatchRuleService = new Mock<IRuleService<PersonModel>>();
            var mockPersonService = new Mock<IPersonService>();
            var person1 = new Mock<PersonModel>().Object;
            var person2 = new Mock<PersonModel>().Object;
            var request = new PersonFraudProbabilityRequest() { FirstPerson = person1, SecondPerson = person2 };

            mockPersonMatchRuleService.Setup(c => c.RunRules(person1, person2)).Returns(new MatchingResult(100));

            var controller = new PersonsController(mockPersonMatchRuleService.Object, mockPersonService.Object);

            // act
            var response = await controller.CalculatePersonFraudProbability(request);

            // assert
            response.Should().NotBeNull("The result should not be null.");
            var result = response.Result.As<OkObjectResult>().Value.As<ApiOkResponse<IMatchingResult>>();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Data.MatchPercentage.Should().Be(100, "The match percentage should match.");
        }

        [Fact]
        public void CalculatePersonFraudProbability_NotOk()
        {
            // arrange
            var mockPersonMatchRuleService = new Mock<IRuleService<PersonModel>>();
            var mockPersonService = new Mock<IPersonService>();
            var controller = new PersonsController(mockPersonMatchRuleService.Object, mockPersonService.Object);

            // act and assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await controller.CalculatePersonFraudProbability(null));
        }
    }
}
