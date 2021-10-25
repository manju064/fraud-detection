// Copyright (c) Friss. All rights reserved.

using System;
using System.Linq;
using System.Threading.Tasks;
using Friss.FraudDetection.Contracts.Models;
using Friss.FraudDetection.DataAccess;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Friss.FraudDetection.Main.Person
{
    /// <inheritdoc />
    public class PersonService : IPersonService
    {
        private readonly FraudDetectionContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonService"/> class.
        /// </summary>
        /// <param name="dbContext">db context to store.</param>
        public PersonService(FraudDetectionContext dbContext)
            => this.dbContext = dbContext;

        /// <inheritdoc/>
        public async Task<PersonModel> GetPersonByIdAsync(int personId)
        {
            var person = await this.dbContext.Persons.Where(p => p.PersonId == personId).FirstOrDefaultAsync();

            return person is null ? null : MapToModel(person);
        }

        /// <inheritdoc/>
        public async Task<PersonModel> CreatePersonAsync(PersonModel personModel)
        {
            var person = MapToEntity(personModel);

            this.dbContext.Persons.Add(person);

            await this.dbContext.SaveChangesAsync();

            return MapToModel(person);
        }

        private static PersonModel MapToModel(DataAccess.Person person)
        {
            TypeAdapterConfig<DataAccess.Person, PersonModel>
                .NewConfig();

            return person.Adapt<PersonModel>();
        }

        private static DataAccess.Person MapToEntity(PersonModel personModel)
        {
            TypeAdapterConfig<PersonModel, DataAccess.Person>
                .NewConfig();

            return personModel.Adapt<DataAccess.Person>();
        }
    }
}
