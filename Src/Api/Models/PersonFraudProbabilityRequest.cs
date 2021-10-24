// Copyright (c) Friss. All rights reserved.

using System.ComponentModel.DataAnnotations;
using Friss.FraudDetection.Contracts.Models;

namespace Friss.FraudDetection.Api.Models
{
    /// <summary>
    /// Request model to calculate probability of identical person.
    /// </summary>
    public class PersonFraudProbabilityRequest
    {
        /// <summary>
        /// Gets or sets first person details.
        /// </summary>
        [Required]
        public Person FirstPerson { get; set; } = null!;

        /// <summary>
        /// Gets or sets second person details.
        /// </summary>
        [Required]
        public Person SecondPerson { get; set; } = null!;
    }
}
