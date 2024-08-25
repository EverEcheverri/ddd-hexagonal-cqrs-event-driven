using EventSagaDriven.Domain.Entities.Genre.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSagaDriven.Infrastructure.EntityFramework.Confgurations.Cosmos;

internal class GenreEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.Genre.Genre>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Genre.Genre> builder)
    {
        builder.Ignore(p => p.DomainEvents);

        builder.HasNoDiscriminator();

        builder
            .ToContainer("Genre")
            .HasPartitionKey(g => g.Id);

        builder.HasKey(g => g.Id);

        builder.HasPartitionKey(g => g.Id);

        builder.Property(g => g.Id).HasConversion<string>();

        builder.Property(g => g.Name)
       .HasColumnName(nameof(Domain.Entities.Genre.Genre.Name))
       .HasConversion<string>(p => p.Value, p => Name.Create(p))
       .HasMaxLength(30);

        builder.Property(g => g.Description)
        .HasColumnName(nameof(Domain.Entities.Genre.Genre.Description))
        .HasConversion<string>(p => p.Value, p => Description.Create(p))
        .HasMaxLength(100);

    }
}
