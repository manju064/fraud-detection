// Copyright (c) Friss. All rights reserved.

using System;
using Friss.FraudDetection.Contracts;
using Friss.FraudDetection.Contracts.Models;
using Friss.FraudDetection.Contracts.Rules;
using Friss.FraudDetection.Contracts.Settings;
using Friss.FraudDetection.Main.Contracts;

namespace Friss.FraudDetection.Main.Rules
{
    /// <summary>
    /// Rule to match Last name of persons.
    /// </summary>
    public class SameFirstNameRule : IMatchingRule<PersonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SameFirstNameRule"/> class.
        /// </summary>
        /// <param name="rulesSettings">Rules settings from configuration.</param>
        public SameFirstNameRule(RulesSettings rulesSettings)
        => this.RuleSettings = rulesSettings[Rule];

        /// <inheritdoc/>
        public RuleSettings RuleSettings { get; }

        private static Rule Rule => Rule.SameFirstName;

        /// <inheritdoc/>
        public IMatchingResult Run(PersonModel firstPerson, PersonModel secondPerson)
        => firstPerson.FirstName.Equals(secondPerson.FirstName) ? new MatchingResult(this.RuleSettings.MatchPercentage) : NoMatchResult.Instance;
    }
}
