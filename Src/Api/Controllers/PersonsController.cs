// Copyright (c) Friss. All rights reserved.

using System.Net;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Friss.FraudDetection.Api.Infrastructure.Filters;
using Friss.FraudDetection.Api.Models;
using Friss.FraudDetection.Api.Models.Responses;
using Friss.FraudDetection.Api.Models.Responses.Exceptions;
using Friss.FraudDetection.Contracts;
using Friss.FraudDetection.Contracts.Models;
using Friss.FraudDetection.Main.Contracts;
using Friss.FraudDetection.Main.Person;
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
        private readonly IRuleService<PersonModel> personMatchRuleService;
        private readonly IPersonService personService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonsController"/> class.
        /// </summary>
        /// <param name="personMatchRuleService">person Matching Rule Service.</param>
        /// <param name="personService">person service for crud operations.</param>
        public PersonsController(IRuleService<PersonModel> personMatchRuleService, IPersonService personService)
        {
            this.personMatchRuleService = personMatchRuleService;
            this.personService = personService;
        }

        /// <summary>
        /// Get person By Id.
        /// </summary>
        /// <param name="id">id of the person to retrieve.</param>
        /// <returns>person details.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiOkResponse<PersonModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestException), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PersonModel>> Get([FromRoute] int id)
        {
            var personModel = await this.personService.GetPersonByIdAsync(id);

            return personModel switch
            {
                null => this.NotFound(new ApiResponse(HttpStatusCode.NotFound, $"Person not found for this id - {id}")),
                _ => this.Ok(new ApiOkResponse<PersonModel>(personModel))
            };
        }

        /// <summary>
        /// Stores a person.
        /// </summary>
        /// <param name="personModel">personModel to save.</param>
        /// <returns>created person details.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiOkResponse<PersonModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestException), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiOkResponse<PersonModel>>> Create([FromBody] PersonModel personModel)
        {
            Guard.Against.Null(personModel, nameof(personModel));

            var createdPersonModel = await this.personService.CreatePersonAsync(personModel);

            return this.Ok(new ApiOkResponse<PersonModel>(createdPersonModel));
        }

        /// <summary>
        /// Calculate fraud probability for 2 persons.
        /// </summary>
        /// <param name="request">Person Fraud Probability Request model.</param>
        /// <returns>response with matching result percentage.</returns>
        [HttpPost("calculatefraudprobability")]
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
