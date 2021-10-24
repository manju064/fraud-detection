// Copyright (c) Friss. All rights reserved.

namespace Friss.FraudDetection.Contracts.Settings
{
    /// <summary>
    /// Matching rule setting.
    /// </summary>
    /// <param name="Enabled">if matching is enabled.</param>
    /// <param name="Priority">Priority of the rule (more the weight is the highest priority).</param>
    /// <param name="MatchPercentage">Match percentage value if rule is successful.</param>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Record types parameters are public members")]
    public record RuleSettings(bool Enabled = true, int Priority = 1, decimal MatchPercentage = 0);
}
