// Copyright (c) Friss. All rights reserved.

using System;

namespace Friss.FraudDetection.DataAccess
{
    /// <summary>
    /// Person entity to persist.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Gets or sets the person Id.
        /// </summary>
        public int PersonId { get; set; }

        /// <summary>
        /// Gets or sets first name of the person.
        /// </summary>
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Gets or sets last name of the person.
        /// </summary>
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Gets or sets identification number of the person.
        /// </summary>
        public int? IdentificationNumber { get; set; }

        /// <summary>
        /// Gets or sets date of birth of the person.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets created date time.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
