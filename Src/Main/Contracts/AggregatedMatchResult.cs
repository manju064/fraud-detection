// Copyright (c) Friss. All rights reserved.

using Friss.FraudDetection.Contracts;

namespace Friss.FraudDetection.Main.Contracts
{
    /// <summary>
    /// Aggregate result of matching.
    /// </summary>
    public class AggregatedMatchResult : IMatchingResult
    {
        private decimal matchPercentage;

        /// <inheritdoc/>
        public decimal MatchPercentage => this.matchPercentage > 100m ? 100m : this.matchPercentage;

        /// <summary>
        /// Adds the match percentage to result.
        /// </summary>
        /// <param name="ruleResult">matching percentage for rule to be added.</param>
        public void AddRuleResult(IMatchingResult ruleResult) => this.matchPercentage = this.matchPercentage + ruleResult.MatchPercentage;
    }
}
