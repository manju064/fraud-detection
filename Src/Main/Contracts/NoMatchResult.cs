// Copyright (c) Friss. All rights reserved.

using Friss.FraudDetection.Contracts;

namespace Friss.FraudDetection.Main.Contracts
{
    /// <summary>
    /// Represents result for no match.
    /// </summary>
    public class NoMatchResult : IMatchingResult
    {
        /// <summary>
        /// No match instance.
        /// </summary>
        public static readonly IMatchingResult Instance = new NoMatchResult();

        /// <inheritdoc/>
        public decimal MatchPercentage { get; } = 0;
    }
}
