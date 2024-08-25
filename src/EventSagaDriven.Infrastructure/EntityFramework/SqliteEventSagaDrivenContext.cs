using EventSagaDriven.Domain.SharedKernel;
using EventSagaDriven.Domain.SharedKernel.Events;
using EventSagaDriven.Infrastructure.EntityFramework.Confgurations.Sqlite;
using EventSagaDriven.Infrastructure.EntityFramework.InitialData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace EventSagaDriven.Infrastructure.EntityFramework;

public class SqliteEventSagaDrivenContext : DbContext
{
    protected readonly IConfiguration _configuration;

    public SqliteEventSagaDrivenContext()
    {

    }

    private readonly MediatR.IPublisher _publisher;
    private readonly ILogger<SqliteEventSagaDrivenContext> _logger;
    public SqliteEventSagaDrivenContext(
        ILogger<SqliteEventSagaDrivenContext> logger,
        IConfiguration configuration,
        DbContextOptions<SqliteEventSagaDrivenContext> options,
        MediatR.IPublisher publisher) : base(options)
    {
        _publisher = publisher;
        _logger = logger;
        _configuration = configuration;
    }

    public DbSet<Domain.Entities.Account.Account> Accounts { get; set; }
    public DbSet<Domain.Entities.Genre.Genre> Genres { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        var events = ChangeTracker.Entries<Entity>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .ToArray();

        foreach (var @event in events)
        {
            var eventTypeClass = @event.GetType();
            if (eventTypeClass.IsGenericType && eventTypeClass.GetGenericTypeDefinition() == typeof(Event<>))
            {
                var eventData = eventTypeClass?.GetProperty("Body")?.GetValue(@event);
                _logger.LogInformation($"Event type: {eventData.GetType()}");

                await _publisher.Publish(eventData, cancellationToken);
            }
        }

        return result;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = GetConnectionStringPath();
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite($"Data Source={connectionString};")
            .LogTo(Console.WriteLine, LogLevel.Information);            
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

    public string GetConnectionStringPath()
    {
        var relativePath = _configuration.GetConnectionString("DefaultConnection");
        var absolutePath = Path.GetFullPath(relativePath!);
        return absolutePath;
    }
}
