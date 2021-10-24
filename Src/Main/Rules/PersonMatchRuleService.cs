// Copyright (c) Friss. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using Friss.FraudDetection.Contracts;
using Friss.FraudDetection.Contracts.Models;
using Friss.FraudDetection.Main.Contracts;

namespace Friss.FraudDetection.Main.Rules
{
    /// <summary>
    /// Rules executor for person.
    /// </summary>
    public class PersonMatchRuleService : IRuleService<Person>
    {
        private readonly IList<IMatchingRule<Person>> matchingRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonMatchRuleService"/> class.
        /// </summary>
        /// <param name="matchingRules">rules to execute.</param>
        public PersonMatchRuleService(IList<IMatchingRule<Person>> matchingRules)
        => this.matchingRules = matchingRules;

        /// <inheritdoc/>
        public IMatchingResult RunRules(Person firstPerson, Person secondPerson)
        {
            var aggregatedMatchResult = new AggregatedMatchResult();

            // Get enabled rules and execute them based on priority.
            foreach (var rule in this.matchingRules.Where(rule => rule.RuleSettings.Enabled).OrderByDescending(rule => rule.RuleSettings.Priority))
            {
                aggregatedMatchResult.AddRuleResult(rule.Run(firstPerson, secondPerson));

                // Break when 100 percentage match is found
                if (aggregatedMatchResult.MatchPercentage == 100m)
                {
                    break;
                }
            }

            return aggregatedMatchResult;
        }
    }
}
