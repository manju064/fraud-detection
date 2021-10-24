// Copyright (c) Friss. All rights reserved.

namespace Friss.FraudDetection.Contracts
{
    /// <summary>
    /// Represents a matching result contract for api.
    /// </summary>
    public interface IMatchingResult
    {
        /// <summary>
        /// Gets match percentage.
        /// </summary>
        public decimal MatchPercentage { get; }
    }
}
