// Copyright (c) Friss. All rights reserved.

using Autofac;
using Friss.FraudDetection.Contracts.Models;
using Friss.FraudDetection.Main.Contracts;
using Friss.FraudDetection.Main.Rules;

namespace Friss.FraudDetection.Api.Modules
{
    /// <summary>
    /// Matching rules module.
    /// </summary>
    public class RulesModule : Module
    {
        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SameIdentificationNumberRule>().As<IMatchingRule<Person>>().SingleInstance();
            builder.RegisterType<SameLastNameRule>().As<IMatchingRule<Person>>().SingleInstance();
            builder.RegisterType<SameFirstNameRule>().As<IMatchingRule<Person>>().SingleInstance();
            builder.RegisterType<SimilarFirstNameRule>().As<IMatchingRule<Person>>().SingleInstance();

            builder.RegisterType<PersonMatchRuleService>().AsImplementedInterfaces().InstancePerDependency();
        }
    }
}
