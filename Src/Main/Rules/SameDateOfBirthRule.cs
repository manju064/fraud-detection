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
    public class SameDateOfBirthRule : IMatchingRule<PersonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SameDateOfBirthRule"/> class.
        /// </summary>
        /// <param name="rulesSettings">Rules settings from configuration.</param>
        public SameDateOfBirthRule(RulesSettings rulesSettings)
        => this.RuleSettings = rulesSettings[Rule];

        /// <inheritdoc/>
        public RuleSettings RuleSettings { get; }

        private static Rule Rule => Rule.SameDateOfBirth;

        /// <inheritdoc/>
        public IMatchingResult Run(PersonModel firstPerson, PersonModel secondPerson)
        => firstPerson.DateOfBirth.HasValue && secondPerson.DateOfBirth.HasValue && firstPerson.DateOfBirth.Value.Date == secondPerson.DateOfBirth.Value.Date ?
            new MatchingResult(this.RuleSettings.MatchPercentage) : NoMatchResult.Instance;
    }
}
