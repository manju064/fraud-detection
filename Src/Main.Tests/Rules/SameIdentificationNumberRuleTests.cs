using FluentAssertions;
using Friss.FraudDetection.Contracts.Models;
using Friss.FraudDetection.Contracts.Rules;
using Friss.FraudDetection.Contracts.Settings;
using Friss.FraudDetection.Main.Contracts;
using Friss.FraudDetection.Main.Rules;
using Xunit;

namespace Friss.FraudDetection.Main.Tests.Rules
{
    public class SameIdentificationNumberRuleTests
    {
        private readonly IMatchingRule<PersonModel> rule;

        public SameIdentificationNumberRuleTests()
        {
            var settings = new RulesSettings
            {
                [Rule.SameIdentificationNumber] = new (true, 1, 100)
            };

            this.rule = new SameIdentificationNumberRule(settings);
        }
            

        [Theory]
        [InlineData(12345, 12345, true)]
        [InlineData(1234, 5678, false)]
        [InlineData(1234, null, false)]
        [InlineData(null, 1234, false)]
        [InlineData(null, null, false)]
        public void Match_Ok(int? idNumber1, int? idNumber2, bool expectedMatch)
        {
            //arrange
            var person1 = new PersonModel() {FirstName = "first", LastName = "last", IdentificationNumber = idNumber1 };
            var person2 = new PersonModel() { FirstName = "second", LastName = "Last", IdentificationNumber = idNumber2 };

            // act
            var result = this.rule.Run(person1, person2);

            // assert
            result.MatchPercentage.Should().Be(expectedMatch ? 100 : 0);
        }
    }
}
