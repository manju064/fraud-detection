// Copyright (c) Friss. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;

namespace Friss.FraudDetection.Contracts.Models
{
    /// <summary>
    /// Api model for Domain object for Person.
    /// </summary>
    public class PersonModel
    {
        /// <summary>
        /// Gets or sets person Id.
        /// </summary>
        public int PersonId { get; set; }

        /// <summary>
        /// Gets or sets first name of the person.
        /// </summary>
        [Required]
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Gets or sets last name of the person.
        /// </summary>
        [Required]
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Gets or sets date of birth of the person.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets identification number of the person.
        /// </summary>
        public int? IdentificationNumber { get; set; }
    }
}
