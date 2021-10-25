// Copyright (c) Friss. All rights reserved.

using Autofac;
using Friss.FraudDetection.Contracts.Models;
using Friss.FraudDetection.DataAccess;
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
            builder.RegisterType<SameIdentificationNumberRule>().As<IMatchingRule<PersonModel>>().SingleInstance();
            builder.RegisterType<SameLastNameRule>().As<IMatchingRule<PersonModel>>().SingleInstance();
            builder.RegisterType<SameFirstNameRule>().As<IMatchingRule<PersonModel>>().SingleInstance();
            builder.RegisterType<SimilarFirstNameRule>().As<IMatchingRule<PersonModel>>().SingleInstance();
            builder.RegisterType<SameDateOfBirthRule>().As<IMatchingRule<PersonModel>>().SingleInstance();

            builder.RegisterType<PersonMatchRuleService>().AsImplementedInterfaces().InstancePerDependency();
        }
    }
}
