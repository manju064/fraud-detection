// Copyright (c) Friss. All rights reserved.

using System.Reflection;
using Friss.FraudDetection.DataAccess.Config;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Friss.FraudDetection.DataAccess
{
    /// <summary>
    /// Fraud Detection db context.
    /// </summary>
    public class FraudDetectionContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FraudDetectionContext"/> class.
        /// </summary>
        /// <param name="options">options.</param>
        public FraudDetectionContext(DbContextOptions<FraudDetectionContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets person table representation.
        /// </summary>
        public DbSet<Person> Persons { get; set; } = null!;

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfiguration(new PersonConfig());
    }
}
