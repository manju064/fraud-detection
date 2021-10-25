// Copyright (c) Friss. All rights reserved.

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
    public class SameIdentificationNumberRule : IMatchingRule<PersonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SameIdentificationNumberRule"/> class.
        /// </summary>
        /// <param name="rulesSettings">Rules settings from configuration.</param>
        public SameIdentificationNumberRule(RulesSettings rulesSettings)
        => this.RuleSettings = rulesSettings[Rule];

        /// <inheritdoc/>
        public RuleSettings RuleSettings { get; }

        private static Rule Rule => Rule.SameIdentificationNumber;

        /// <inheritdoc/>
        public IMatchingResult Run(PersonModel firstPerson, PersonModel secondPerson)
            => firstPerson.IdentificationNumber.HasValue && secondPerson.IdentificationNumber.HasValue && firstPerson.IdentificationNumber.Value == secondPerson.IdentificationNumber.Value ?
                new MatchingResult(this.RuleSettings.MatchPercentage) : NoMatchResult.Instance;
    }
}
