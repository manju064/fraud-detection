// Copyright (c) Friss. All rights reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Friss.FraudDetection.DataAccess.Config
{
    /// <summary>
    /// Configuration for person table and columns.
    /// </summary>
    public class PersonConfig : IEntityTypeConfiguration<Person>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(e => e.PersonId);

            builder.Property(n => n.FirstName).HasMaxLength(128).IsRequired();
            builder.Property(n => n.LastName).HasMaxLength(128).IsRequired();
            builder.Property(n => n.CreatedAt).IsRequired();

            builder.HasIndex(n => n.CreatedAt).IsUnique(false);
        }
    }
}
