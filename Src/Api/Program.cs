// Copyright (c) Friss. All rights reserved.

using System;
using System.IO;
using System.Reflection;
using Autofac.Extensions.DependencyInjection;
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
        {
            var baseConfigPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException("Null base config path.");

            CreateHostBuilder(args, baseConfigPath).Build().Run();
        }

        /// <summary>
        /// Create host builder.
        /// </summary>
        /// <param name="args">arguments for startup.</param>
        /// <param name="baseConfigPath">location of the base config file.</param>
        /// <returns>configured host builder.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args, string baseConfigPath) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
