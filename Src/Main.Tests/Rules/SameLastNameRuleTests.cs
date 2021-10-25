using FluentAssertions;
using Friss.FraudDetection.Contracts.Models;
using Friss.FraudDetection.Contracts.Rules;
using Friss.FraudDetection.Contracts.Settings;
using Friss.FraudDetection.Main.Contracts;
using Friss.FraudDetection.Main.Rules;
using Xunit;

namespace Friss.FraudDetection.Main.Tests.Rules
{
    public class SameLastNameRuleTests
    {
        private readonly IMatchingRule<PersonModel> rule;

        public SameLastNameRuleTests()
        {
            var settings = new RulesSettings
            {
                [Rule.SameLastName] = new (true, 1, 40)
            };

            this.rule = new SameLastNameRule(settings);
        }
            

        [Theory]
        [InlineData("Keshava", "Keshava", true)]
        [InlineData("Craw", "Craw", true)]
        [InlineData("Craw", "test", false)]
        public void Match_Ok(string firstPersonLastName, string secondPersonLastName, bool expectedMatch)
        {
            //arrange
            var person1 = new PersonModel() {FirstName = "first", LastName = firstPersonLastName };
            var person2 = new PersonModel() { FirstName = "second", LastName = secondPersonLastName };

            // act
            var result = this.rule.Run(person1, person2);

            // assert
            result.MatchPercentage.Should().Be(expectedMatch ? 40 : 0);
        }
    }
}
