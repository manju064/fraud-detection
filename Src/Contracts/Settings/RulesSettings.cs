// Copyright (c) Friss. All rights reserved.

using System.Collections.Generic;
using Friss.FraudDetection.Contracts.Rules;
using Microsoft.Extensions.Configuration;

namespace Friss.FraudDetection.Contracts.Settings
{
    /// <summary>
    /// Rules settings from configuration.
    /// </summary>
    public class RulesSettings : Dictionary<string, RuleSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RulesSettings"/> class.
        /// </summary>
        public RulesSettings()
        {
            foreach (var rule in Rule.List)
            {
                this[rule] = new RuleSettings();
            }
        }

        /// <summary>
        /// Factory to uild configuration settings.
        /// </summary>
        public class Factory
        {
            private readonly IConfiguration config;

            /// <summary>
            /// Initializes a new instance of the <see cref="Factory"/> class.
            /// </summary>
            /// <param name="config">configuration.</param>
            public Factory(IConfiguration config) => this.config = config;

            /// <summary>
            /// build rule settings from configuration.
            /// </summary>
            /// <returns>Rules settings.</returns>
            public RulesSettings Build()
            {
                var section = this.config.GetSection("RulesSettings");
                var settings = new RulesSettings();
                section.Bind(settings);

                return settings;
            }
        }
    }
}
