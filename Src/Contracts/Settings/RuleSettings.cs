// Copyright (c) Friss. All rights reserved.

namespace Friss.FraudDetection.Contracts.Settings
{
    /// <summary>
    /// Matching rule setting.
    /// </summary>
    public record RuleSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RuleSettings"/> class.
        /// </summary>
        public RuleSettings()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleSettings"/> class.
        /// </summary>
        /// <param name="enabled">indicating whether matching is enabled.</param>
        /// <param name="priority">priority of the rule.</param>
        /// <param name="matchPercentage">match percentage value.</param>
        public RuleSettings(bool enabled = true, int priority = 1, decimal matchPercentage = 0)
        {
            this.Enabled = enabled;
            this.Priority = priority;
            this.MatchPercentage = matchPercentage;
        }

        /// <summary>
        /// Gets or sets a value indicating whether matching is enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        ///  Gets or sets priority of the rule (more the weight is the highest priority).
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets match percentage value if rule is successful.
        /// </summary>
        public decimal MatchPercentage { get; set; }
    }
}
