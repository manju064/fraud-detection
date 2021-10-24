// Copyright (c) Friss. All rights reserved.

using Ardalis.SmartEnum;

namespace Friss.FraudDetection.Contracts.Rules
{
    /// <summary>
    /// Rule configuration key.
    /// </summary>
    public class Rule : SmartEnum<Rule, string>
    {
        /// <summary>
        /// same Identification Number rule.
        /// </summary>
        public static readonly Rule SameIdentificationNumber = new ("SameIdentificationNumber");

        /// <summary>
        /// same Last name rule.
        /// </summary>
        public static readonly Rule SameLastName = new ("SameLastName");

        /// <summary>
        /// same first name rule.
        /// </summary>
        public static readonly Rule SameFirstName = new ("SameFirstName");

        /// <summary>
        /// similar first name rule.
        /// </summary>
        public static readonly Rule SimilarFirstName = new ("SimilarFirstName");

        /// <summary>
        /// same date of birth rule.
        /// </summary>
        public static readonly Rule SameDateOfBirth = new ("SameDateOfBirth");

        /// <summary>
        /// Initializes a new instance of the <see cref="Rule"/> class.
        /// </summary>
        /// <param name="ruleKey">rule key.</param>
        public Rule(string ruleKey)
            : base(ruleKey, ruleKey)
        {
        }
    }
}
