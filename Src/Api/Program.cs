// Copyright (c) Friss. All rights reserved.

using System;
using Autofac.Extensions.DependencyInjection;
using Friss.FraudDetection.Api.Extensions;
using Friss.FraudDetection.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Friss.FraudDetection.Api
{
    /// <summary>
    /// Entry point class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point for api.
        /// </summary>
        /// <param name="args">arguments for startup.</param>
        /// <exception cref="InvalidOperationException">Throws error if can't find execution assembly location.</exception>
        public static void Main(string[] args)
            => CreateHostBuilder(args).Build()
                .MigrateDatabase<FraudDetectionContext>()
                .Run();

        /// <summary>
        /// Create host builder.
        /// </summary>
        /// <param name="args">arguments for startup.</param>
        /// <returns>configured host builder.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
