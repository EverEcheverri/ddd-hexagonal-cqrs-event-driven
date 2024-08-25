using EventSagaDriven.Application.Account.Interfaces;
using EventSagaDriven.Application.Account.UseCases;
using EventSagaDriven.Application.ThirdPartyBook.Interfaces;
using EventSagaDriven.Application.ThirdPartyBook.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace EventSagaDriven.Application.DependencyInjection;

public static class RegisterUseCasesExtensions
{
    public static void AddUseCases(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateAccount, CreateAccountUseCase>()
            .AddScoped<IGetAccountByEmail, GetAccountByEmailUseCase>()
            .AddScoped<IGetThirdPartyBooks, GetThirdPartyBooksUseCase>()
            .AddScoped<IAddAccountGenres, AddAccountGenresUseCase>();
    }

    public static void AddEventHandlersUseCases(this IServiceCollection services)
    {
        services.AddMediatR(mtr =>
        {
            mtr.RegisterServicesFromAssembly(typeof(AccountCreatedIntegrationEventUseCase).Assembly);
            mtr.RegisterServicesFromAssembly(typeof(SendWelcomeNotificationUseCase).Assembly);
        });
    }
}
