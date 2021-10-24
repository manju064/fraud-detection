// Copyright (c) Friss. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Friss.FraudDetection.Contracts;
using Friss.FraudDetection.Contracts.Models;
using Friss.FraudDetection.Contracts.Settings;
using Friss.FraudDetection.Main.Contracts;
using Friss.FraudDetection.Main.Rules;
using Moq;
using Xunit;

namespace Friss.FraudDetection.Main.Tests.Rules
{
    /// <summary>
    /// Rules executor for person.
    /// </summary>
    public class PersonMatchRuleServiceTests 
    {
        [Fact]
        public void RunRules_Ok()
        {
            // arrange
            var person1 = new Mock<Person>().Object;
            var person2 = new Mock<Person>().Object;

            var rule1 = new Mock<IMatchingRule<Person>>();
            var rule2 = new Mock<IMatchingRule<Person>>();
            var rule3 = new Mock<IMatchingRule<Person>>();

            rule1.SetupGet(r => r.RuleSettings).Returns(new RuleSettings(true, 2, 40));
            rule1.Setup(r => r.Run(person1, person2)).Returns(new MatchingResult(40));

            rule2.SetupGet(r => r.RuleSettings).Returns(new RuleSettings(true, 1, 40));
            rule2.Setup(r => r.Run(person1, person2)).Returns(new MatchingResult(40));

            rule3.SetupGet(r => r.RuleSettings).Returns(new RuleSettings(true, 1, 20));
            rule3.Setup(r => r.Run(person1, person2)).Returns(NoMatchResult.Instance);


            IRuleService<Person> ruleService = new PersonMatchRuleService(new List<IMatchingRule<Person>>(){ rule1.Object, rule2.Object});

            // act
            var result = ruleService.RunRules(person1, person2);

            result.Should().NotBeNull("The result should not be null.");
            result.MatchPercentage.Should().Be(80, "The match percentage should match.");
        }

        [Fact]
        public void RunRules_MultipleMatches_Ok()
        {
            // arrange
            var person1 = new Mock<Person>().Object;
            var person2 = new Mock<Person>().Object;

            var rule1 = new Mock<IMatchingRule<Person>>();
            var rule2 = new Mock<IMatchingRule<Person>>();
            var rule3 = new Mock<IMatchingRule<Person>>();

            rule1.SetupGet(r => r.RuleSettings).Returns(new RuleSettings(true, 2, 100));
            rule1.Setup(r => r.Run(person1, person2)).Returns(new MatchingResult(100));

            rule2.SetupGet(r => r.RuleSettings).Returns(new RuleSettings(true, 1, 40));
            rule2.Setup(r => r.Run(person1, person2)).Returns(new MatchingResult(40));

            rule3.SetupGet(r => r.RuleSettings).Returns(new RuleSettings(true, 1, 40));
            rule3.Setup(r => r.Run(person1, person2)).Returns(new MatchingResult(40));


            IRuleService<Person> ruleService = new PersonMatchRuleService(new List<IMatchingRule<Person>>() { rule1.Object, rule2.Object });

            // act
            var result = ruleService.RunRules(person1, person2);

            result.Should().NotBeNull("The result should not be null.");
            result.MatchPercentage.Should().Be(100, "The match percentage should match.");
        }
    }
}
