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
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Friss.FraudDetection.Api.Tests
{
    public class PersonsControllerTests
    {
        [Fact]
        public async Task CalculatePersonFraudProbability_Ok()
        {
            // arrange
            var mockPersonMatchRuleService = new Mock<IRuleService<Person>>();
            var person1 = new Mock<Person>().Object;
            var person2 = new Mock<Person>().Object;
            var request = new PersonFraudProbabilityRequest() { FirstPerson = person1, SecondPerson = person2 };

            mockPersonMatchRuleService.Setup(c => c.RunRules(person1, person2)).Returns(new MatchingResult(100));

            var controller = new PersonsController(mockPersonMatchRuleService.Object);

            var response = await controller.CalculatePersonFraudProbability(request);

            response.Should().NotBeNull("The result should not be null.");
            var result = response.Result.As<OkObjectResult>().Value.As<ApiOkResponse<IMatchingResult>>();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Data.MatchPercentage.Should().Be(100, "The match percentage should match.");
        }

        [Fact]
        public void CalculatePersonFraudProbability_NotOk()
        {
            // arrange
            var mockPersonMatchRuleService = new Mock<IRuleService<Person>>();

            var controller = new PersonsController(mockPersonMatchRuleService.Object);

            // act and assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await controller.CalculatePersonFraudProbability(request: null));
        }
    }
}
