// Copyright (c) Friss. All rights reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Friss.FraudDetection.DataAccess
{
    /// <summary>
    /// FraudDetectionContext Factory.
    /// </summary>
    public class FraudDetectionContextFactory : IDesignTimeDbContextFactory<FraudDetectionContext>
    {
        /// <inheritdoc/>
        public FraudDetectionContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FraudDetectionContext>();
            optionsBuilder.UseSqlite("Data Source=FraudDetection.db");

            return new FraudDetectionContext(optionsBuilder.Options);
        }
    }

}
