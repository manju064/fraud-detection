// Copyright (c) Friss. All rights reserved.

using Friss.FraudDetection.Contracts;
using Friss.FraudDetection.Contracts.Models;
using Friss.FraudDetection.Contracts.Rules;
using Friss.FraudDetection.Contracts.Settings;
using Friss.FraudDetection.Main.Contracts;
using FuzzySharp;

namespace Friss.FraudDetection.Main.Rules
{
    /// <summary>
    /// Rule to match Last name of persons.
    /// </summary>
    public class SimilarFirstNameRule : IMatchingRule<Person>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimilarFirstNameRule"/> class.
        /// </summary>
        /// <param name="rulesSettings">Rules settings from configuration.</param>
        public SimilarFirstNameRule(RulesSettings rulesSettings)
        => this.RuleSettings = rulesSettings[Rule];

        /// <inheritdoc/>
        public RuleSettings RuleSettings { get; }

        private static Rule Rule => Rule.SimilarFirstName;

        /// <inheritdoc/>
        public IMatchingResult Run(Person firstPerson, Person secondPerson)
        {
            var score = Fuzz.PartialRatio(firstPerson.FirstName, secondPerson.FirstName);

            if (score > 70)
            {
                return new MatchingResult(this.RuleSettings.MatchPercentage);
            }

            return NoMatchResult.Instance;
        }
    }
}
