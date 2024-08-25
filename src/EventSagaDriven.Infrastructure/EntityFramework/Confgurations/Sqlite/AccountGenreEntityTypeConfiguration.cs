using EventSagaDriven.Domain.Entities.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSagaDriven.Infrastructure.EntityFramework.Confgurations.Sqlite;

internal class AccountGenreEntityTypeConfiguration : IEntityTypeConfiguration<AccountGenre>
{
    public void Configure(EntityTypeBuilder<AccountGenre> builder)
    {
        builder.ToTable("AccountGenres");
        builder.HasKey("AccountId", "GenreId");

        //builder.Property(c => c.AccountId).HasConversion<Guid>();
        //builder.Property(c => c.GenreId).HasConversion<Guid>();

        builder.HasOne(pt => pt.Genre)
          .WithMany(p => p.AccountGenres)
          .HasForeignKey("GenreId");
        builder.HasOne(pt => pt.Account)
          .WithMany(a => a.AccountGenres)
          //.WithMany(Domain.Entities.Account.Account.AccountGenresName)
          .HasForeignKey("AccountId");

    }
}
