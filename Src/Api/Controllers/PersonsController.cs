// Copyright (c) Friss. All rights reserved.

using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Friss.FraudDetection.Api.Infrastructure.Filters;
using Friss.FraudDetection.Api.Models;
using Friss.FraudDetection.Api.Models.Responses;
using Friss.FraudDetection.Api.Models.Responses.Exceptions;
using Friss.FraudDetection.Contracts;
using Friss.FraudDetection.Contracts.Models;
using Friss.FraudDetection.Main.Contracts;
using Friss.FraudDetection.Main.Rules;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Friss.FraudDetection.Api.Controllers
{
    /// <summary>
    /// Api end point to interact with Person data.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IRuleService<Person> personMatchRuleService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonsController"/> class.
        /// </summary>
        /// <param name="personMatchRuleService">person Matching Rule Service.</param>
        public PersonsController(IRuleService<Person> personMatchRuleService)
            => this.personMatchRuleService = personMatchRuleService;

        /// <summary>
        /// Calculate fraud probability for 2 persons.
        /// </summary>
        /// <param name="request">Person Fraud Probability Request model.</param>
        /// <returns>response with matching result percentage.</returns>
        [HttpPost("/calculatefraudprobability")]
        [ProducesResponseType(typeof(ApiOkResponse<IMatchingResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestException), StatusCodes.Status400BadRequest)]
        [ValidateModelState]
#pragma warning disable 1998
        public async Task<ActionResult<ApiOkResponse<IMatchingResult>>> CalculatePersonFraudProbability([FromBody] PersonFraudProbabilityRequest request)
#pragma warning restore 1998
        {
            Guard.Against.Null(request, nameof(request));

            var response = this.personMatchRuleService.RunRules(request.FirstPerson, request.SecondPerson);

            return this.Ok(new ApiOkResponse<IMatchingResult>(response));
        }
    }
}
