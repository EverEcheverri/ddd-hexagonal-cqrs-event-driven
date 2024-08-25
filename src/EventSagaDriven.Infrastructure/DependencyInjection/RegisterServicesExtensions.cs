using EventSagaDriven.Domain.Entities.Account.Repositories;
using EventSagaDriven.Domain.Entities.Genre.Repositories;
using EventSagaDriven.Domain.ExternalBooksService;
using EventSagaDriven.Domain.SharedKernel.Events;
using EventSagaDriven.Infrastructure.EntityFramework.Interfaces;
using EventSagaDriven.Infrastructure.MessageBus.EventGrid;
using EventSagaDriven.Infrastructure.Models.AppSettings;
using EventSagaDriven.Infrastructure.Services;
using EventSagaDriven.Infrastructure.Services.BigBookApi;
using EventSagaDriven.Infrastructure.Services.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EventSagaDriven.Infrastructure.DependencyInjection;

public static class RegisterServicesExtensions
{
    public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services
        .AddScoped<IInfrastructureAccountRepository, EntityFramework.Account.Repositories.Sqlite.SqliteAccountRepository>()
        .AddScoped<IInfrastructureAccountRepository, EntityFramework.Account.Repositories.Cosmos.CosmosAccountRepository>()
        .AddScoped<IAccountRepository, EntityFramework.Account.Repositories.AccountRepository>()
        .AddScoped<IGenreRepository, EntityFramework.Genre.Repositories.Sqlite.SqliteGenreRepository>()
        .AddScoped<IThirdPartyBookService, ExternalBooksService.ExternalBooksService>()

        .AddScoped<IPublisher, Publisher>();

        services.RegisterGetAuthToken(configuration);
        services.RegisterGetBigBookApi(configuration);
    }

    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

    private static void RegisterGetAuthToken(this IServiceCollection services, IConfiguration configuration)
    {
        var apiBigBookOptions = configuration.GetSection(ApiBigBookOptions.ApiBigBookOptionsName)
                .Get<ApiBigBookOptions>();

        var authInfo = apiBigBookOptions!.GetBasicAuth();

        services.AddRefitClient<IBigBookApiAuthService>(new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(JsonSerializerOptions),
        })
        .ConfigureHttpClient(client =>
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = apiBigBookOptions!.Service.Scheme,
                Host = apiBigBookOptions.OauthBaseUrl,
                Path = apiBigBookOptions.OauthClientKey
            };

            client.BaseAddress = uriBuilder.Uri;
            //client.DefaultRequestHeaders.Add("Authorization", $"Basic {authInfo}");
        });
    }

    private static void RegisterGetBigBookApi(this IServiceCollection services, IConfiguration configuration)
    {
        var apiBigBookOptions = configuration.GetSection(ApiBigBookOptions.ApiBigBookOptionsName)
                .Get<ApiBigBookOptions>();

        services.AddRefitClient<IBigBookApiService>(new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(JsonSerializerOptions),
        })
        .ConfigureHttpClient(client =>
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = apiBigBookOptions!.Service.Scheme,
                Host = apiBigBookOptions.Service.Host,
                Path = apiBigBookOptions.Service.Path
            };

            client.BaseAddress = uriBuilder.Uri;
            client.DefaultRequestHeaders.Add("x-api-key", apiBigBookOptions.ApiKey);
        })
        .AddHttpMessageHandler<BigBookApiHandler>();

        services.AddSingleton<IAuthTokenStore, AuthTokenStore>();
        services.AddTransient<BigBookApiHandler>();
    }

    private static string GetBasicAuth(this ApiBigBookOptions apiBigBookOptions)
    {
        if (apiBigBookOptions.UserName is null || apiBigBookOptions.Password is null)
        {
            throw new Exception();
        }

        var bytes = Encoding.UTF8.GetBytes($"{apiBigBookOptions.UserName}:{apiBigBookOptions.Password}");

        return Convert.ToBase64String(bytes);
    }
}

