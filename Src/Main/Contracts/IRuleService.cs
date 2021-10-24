// Copyright (c) Friss. All rights reserved.

using Friss.FraudDetection.Contracts;

namespace Friss.FraudDetection.Main.Contracts
{
    /// <summary>
    /// Allows execution of rules against given data.
    /// </summary>
    /// <typeparam name="T">The type of the objects.</typeparam>
    public interface IRuleService<in T>
    {
        /// <summary>
        /// Run matching rules for the given objects..
        /// </summary>
        /// <param name="firstObject">first object.</param>
        /// <param name="secondObject">second object.</param>
        /// <returns>matching rule based on rule success.</returns>
        IMatchingResult RunRules(T firstObject, T secondObject);
    }
}
