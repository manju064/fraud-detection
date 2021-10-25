using FluentAssertions;
using Friss.FraudDetection.Contracts.Models;
using Friss.FraudDetection.Contracts.Rules;
using Friss.FraudDetection.Contracts.Settings;
using Friss.FraudDetection.Main.Contracts;
using Friss.FraudDetection.Main.Rules;
using Xunit;

namespace Friss.FraudDetection.Main.Tests.Rules
{
    public class SimilarFirstNameRuleTests
    {
        private readonly IMatchingRule<PersonModel> rule;

        public SimilarFirstNameRuleTests()
        {
            var settings = new RulesSettings
            {
                [Rule.SimilarFirstName] = new (true, 1, 15)
            };

            this.rule = new SimilarFirstNameRule(settings);
        }
            

        [Theory]
        [InlineData("Andrew", "A", true)]
        [InlineData("Andrew", "Andew", true)]
        [InlineData("Andrew", "Andy", true)]
        [InlineData("Manju", "Keshava", false)]
        public void Match_Ok(string firstPersonFirstName, string secondPersonFirstName, bool expectedMatch)
        {
            //arrange
            var person1 = new PersonModel() {FirstName = firstPersonFirstName, LastName = "lastname"};
            var person2 = new PersonModel() { FirstName = secondPersonFirstName, LastName = "lastname" };

            // act
            var result = this.rule.Run(person1, person2);

            // assert
            result.MatchPercentage.Should().Be(expectedMatch ? 15 : 0);
        }
    }
}
