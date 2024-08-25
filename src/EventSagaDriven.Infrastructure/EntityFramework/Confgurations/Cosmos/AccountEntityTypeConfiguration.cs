using EventSagaDriven.Domain.Entities.Account.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSagaDriven.Infrastructure.EntityFramework.Configurations.Cosmos;

internal class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.Account.Account>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Account.Account> builder)
    {
        builder.Ignore(p => p.DomainEvents);

        builder.HasNoDiscriminator();

        builder.HasPartitionKey(nameof(Domain.Entities.Account.Account.Id));

        builder
            .ToContainer("Account")
            .HasPartitionKey(a => a.Id);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion<string>();

        builder.HasIndex(c => c.Email)
            .IsUnique();

        builder.Property(p => p.Email)
       .HasColumnName(nameof(Domain.Entities.Account.Account.Email))
       .HasConversion<string>(p => p.Value, p => Email.Create(p))
       .HasMaxLength(255);

        builder.Property(p => p.UserName)
        .HasColumnName(nameof(Domain.Entities.Account.Account.UserName))
        .HasConversion<string>(p => p.Value, p => UserName.Create(p))
        .HasMaxLength(255);

        builder.Property(p => p.Mobile)
       .HasColumnName(nameof(Domain.Entities.Account.Account.Mobile))
       .HasConversion<string>(p => p.Value, p => Mobile.Create(p))
       .HasMaxLength(255);

        builder.Property(c => c.AccountType).HasConversion<int>();

        builder.Property(c => c.CityId).HasConversion<string>();

        builder.Metadata.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
