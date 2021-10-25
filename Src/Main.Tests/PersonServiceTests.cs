using System;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using Friss.FraudDetection.Contracts.Models;
using Friss.FraudDetection.DataAccess;
using Friss.FraudDetection.Main.Person;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Friss.FraudDetection.Main.Tests
{
    public class PersonServiceTests
    {
        private readonly IPersonService personService;
        private readonly FraudDetectionContext testDbContext;

        public PersonServiceTests()
        {
            this.testDbContext = this.GetTestDatabase();
            this.personService = new PersonService(this.testDbContext);
        }

        [Fact]
        public async Task StorePerson_Ok()
        {
            // arrange
            var personModel = new PersonModel() { FirstName = "first", LastName = "LastName", DateOfBirth = DateTime.Today.AddYears(-20), IdentificationNumber = 1234 };

            // act
            var result = await this.personService.CreatePersonAsync(personModel);

            // assert
            result.Should().NotBeNull();
            result.PersonId.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetPerson_Ok()
        {
            // arrange
            var person = new DataAccess.Person()
                {FirstName = "first", LastName = "LastName", DateOfBirth = DateTime.Today.AddYears(-20), IdentificationNumber = 1234};
            this.testDbContext.Persons.Add(person);
            await this.testDbContext.SaveChangesAsync();

            // act
            var result = this.personService.GetPersonByIdAsync(person.PersonId);

            // assert
            result.Should().NotBeNull();
            result.Id.Should().Be(person.PersonId);
        }

        [Fact]
        public async Task GetPerson_NotOk()
        {
            // act
            var result = await this.personService.GetPersonByIdAsync(12345677);

            // assert
            result.Should().BeNull();
        }

        private FraudDetectionContext GetTestDatabase()
        {
            // In-memory database only exists while the connection is open
            var conn = new SqliteConnection(GetSqliteConnectionString());
            conn.Open();

            var options = new DbContextOptionsBuilder<FraudDetectionContext>()
                .UseSqlite(conn)
                .Options;
            var db = new FraudDetectionContext(options);

            db.Database.EnsureCreated();

            return db;
        }

        private static string GetSqliteConnectionString()
        {
            var memphisCtxSqlitePath = Path.GetTempFileName().Replace(".tmp", ".FraudDetection.sqlite");
            var memphisCtxConnStringBuilder = new SqliteConnectionStringBuilder()
            {
                { "data source", memphisCtxSqlitePath },
            };
            return memphisCtxConnStringBuilder.ConnectionString;
        }
    }
}
