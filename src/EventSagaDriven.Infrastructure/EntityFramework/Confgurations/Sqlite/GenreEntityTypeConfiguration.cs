using EventSagaDriven.Domain.Entities.Genre.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSagaDriven.Infrastructure.EntityFramework.Confgurations.Sqlite;

internal class GenreEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.Genre.Genre>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Genre.Genre> builder)
    {
        builder.Ignore(p => p.DomainEvents);

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion<Guid>();

        builder.Property(p => p.Name)
       .HasColumnName(nameof(Domain.Entities.Genre.Genre.Name))
       .HasConversion<string>(p => p.Value, p => Name.Create(p))
       .HasMaxLength(30);

        builder.Property(p => p.Description)
        .HasColumnName(nameof(Domain.Entities.Genre.Genre.Description))
        .HasConversion<string>(p => p.Value, p => Description.Create(p))
        .HasMaxLength(100);

        builder.Metadata.FindNavigation(nameof(Domain.Entities.Genre.Genre.AccountGenres))
       .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
