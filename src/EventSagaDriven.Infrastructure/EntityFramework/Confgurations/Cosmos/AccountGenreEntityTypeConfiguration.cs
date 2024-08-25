using EventSagaDriven.Domain.Entities.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSagaDriven.Infrastructure.EntityFramework.Confgurations.Cosmos;

internal class AccountGenreEntityTypeConfiguration : IEntityTypeConfiguration<AccountGenre>
{
    public void Configure(EntityTypeBuilder<AccountGenre> builder)
    {       
        builder.HasNoDiscriminator();

        builder.ToContainer("AccountGenres");
        builder.HasKey("AccountId", "GenreId");
        
        //Relationships
        builder.HasOne(pt => pt.Genre)
          .WithMany(Domain.Entities.Genre.Genre.AccountGenresName)
          .HasForeignKey("GenreId");

        builder.HasOne(pt => pt.Account)
          .WithMany(Domain.Entities.Account.Account.AccountGenresName)
          .HasForeignKey("AccountId");

    }
}
