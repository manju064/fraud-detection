using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Friss.FraudDetection.Api.Extensions
{
    /// <summary>
    /// Web host extensions.
    /// </summary>
    public static class WebHostExtensions
    {
        /// <summary>
        /// Check if migration should be applied and applies migration.
        /// </summary>
        /// <typeparam name="T">DbContext.</typeparam>
        /// <param name="host">host.</param>
        /// <returns>IWebHost.</returns>
        public static IHost MigrateDatabase<T>(this IHost host)
            where T : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var db = services.GetRequiredService<T>();
                    db.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An ERROR occurred while migrating the DATABASE.");
                }
            }

            return host;
        }
    }
}
