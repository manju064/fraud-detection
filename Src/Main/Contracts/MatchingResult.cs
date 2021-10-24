// Copyright (c) Friss. All rights reserved.

using Friss.FraudDetection.Contracts;

namespace Friss.FraudDetection.Main.Contracts
{
    /// <summary>
    /// Match result instance.
    /// </summary>
    public class MatchingResult : IMatchingResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatchingResult"/> class.
        /// </summary>
        /// <param name="matchPercentage">allocated match percentage.</param>
        public MatchingResult(decimal matchPercentage)
            => this.MatchPercentage = matchPercentage;

        /// <inheritdoc/>
        public decimal MatchPercentage { get; }
    }
}
