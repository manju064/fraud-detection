// Copyright (c) Friss. All rights reserved.

using Friss.FraudDetection.Contracts;
using Friss.FraudDetection.Contracts.Settings;

namespace Friss.FraudDetection.Main.Contracts
{
    /// <summary>
    /// Matching rule to perform match.
    /// </summary>
    /// <typeparam name="T">Object type to compare.</typeparam>
    public interface IMatchingRule<in T>
    {
        /// <summary>
        /// Gets the rule setting.
        /// </summary>
        RuleSettings RuleSettings { get; }

        /// <summary>
        /// Runs the rule.
        /// </summary>
        /// <param name="firstObject">First  object to compare.</param>
        /// <param name="secondObject">second object to compare.</param>
        /// <returns>Matching result.</returns>
        IMatchingResult Run(T firstObject, T secondObject);
    }
}
