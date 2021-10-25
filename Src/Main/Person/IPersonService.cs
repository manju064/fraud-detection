// Copyright (c) Friss. All rights reserved.

using System.Threading.Tasks;

namespace Friss.FraudDetection.Main.Person
{
    /// <summary>
    /// Person Service to CRUD operations.
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Get Person By Id.
        /// </summary>
        /// <param name="personId">person Id.</param>
        /// <returns>Person</returns>
        Task<FraudDetection.Contracts.Models.PersonModel> GetPersonByIdAsync(int personId);

        /// <summary>
        /// create/Store person.
        /// </summary>
        /// <param name="person">person object to save.</param>
        /// <returns>created person with id.</returns>
        Task<FraudDetection.Contracts.Models.PersonModel> CreatePersonAsync(FraudDetection.Contracts.Models.PersonModel person);
    }
}
