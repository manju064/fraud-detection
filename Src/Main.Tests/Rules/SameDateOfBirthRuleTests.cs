using System;
using FluentAssertions;
using Friss.FraudDetection.Contracts.Models;
using Friss.FraudDetection.Contracts.Rules;
using Friss.FraudDetection.Contracts.Settings;
using Friss.FraudDetection.Main.Contracts;
using Friss.FraudDetection.Main.Rules;
using Xunit;

namespace Friss.FraudDetection.Main.Tests.Rules
{
    public class SameDateOfBirthRuleTests
    {
        private readonly IMatchingRule<PersonModel> rule;

        public SameDateOfBirthRuleTests()
        {
            var settings = new RulesSettings
            {
                [Rule.SameDateOfBirth] = new (true, 1, 40)
            };

            this.rule = new SameDateOfBirthRule(settings);
        }

        public static readonly object[][] CorrectData =
        {
            new object[] { new DateTime(2021,10,24), new DateTime(2021,10,24), true },
            new object[] { new DateTime(2021,10,24), null, false },
            new object[] { null, new DateTime(2021,10,24), false },
            new object[] { null, null, false },
        };

        [Theory, MemberData(nameof(CorrectData))]
        public void Match_Ok(DateTime? dob1, DateTime? dob2, bool expectedMatch)
        {
            //arrange
            var person1 = new PersonModel() {FirstName = "first", LastName = "last",  DateOfBirth = dob1 };
            var person2 = new PersonModel() { FirstName = "second", LastName = "Last", DateOfBirth = dob2 };

            // act
            var result = this.rule.Run(person1, person2);

            // assert
            result.MatchPercentage.Should().Be(expectedMatch ? 40 : 0);
        }
    }
}
