using EventSagaDriven.Infrastructure.EntityFramework.Confgurations.Cosmos;
using EventSagaDriven.Infrastructure.EntityFramework.Configurations.Cosmos;
using EventSagaDriven.Infrastructure.EntityFramework.InitialData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace EventSagaDriven.Infrastructure.EntityFramework;

public class CosmosEventSagaDrivenContext : DbContext
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<CosmosEventSagaDrivenContext> _logger;

    public CosmosEventSagaDrivenContext(
    DbContextOptions<CosmosEventSagaDrivenContext> options,
    ILogger<CosmosEventSagaDrivenContext> logger,
    IConfiguration configuration) : base(options)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public DbSet<Domain.Entities.Account.Account> Accounts { get; set; }
    public DbSet<Domain.Entities.Genre.Genre> Genres { get; set; }
    public DbSet<Domain.Entities.Account.AccountGenre> AccountGenres { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        if (!optionsBuilder.IsConfigured)
        {
            base.OnConfiguring(optionsBuilder);

            var connectionString = _configuration.GetConnectionString("CosmosConnectionString");

            optionsBuilder.UseCosmos(
                connectionString!,
                databaseName: "accountdb"
                );
        }

        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.ApplyConfiguration(new AccountEntityTypeConfiguration());
        builder.ApplyConfiguration(new GenreEntityTypeConfiguration());
        builder.ApplyConfiguration(new AccountGenreEntityTypeConfiguration());

        var genres = InitialGenres.GetGenres();
        builder.Entity<Domain.Entities.Genre.Genre>().HasData(genres);

        base.OnModelCreating(builder);
    }
}
