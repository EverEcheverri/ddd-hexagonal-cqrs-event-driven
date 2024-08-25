using EventSagaDriven.Application.Account.Exceptions;
using EventSagaDriven.Domain.Entities.Account.Exceptions;
using Microsoft.Net.Http.Headers;
using System.Net;

namespace EventSagaDriven.Api.Middleware;

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }

    public static Task EventSchedulingErrorResponseAsync(this HttpResponse response,
      Exception businessException)
    {
        var (httpStatusCode, eventId) = GetResponseCode(businessException.GetType().Name);
        var message = $"{{\"code\": {eventId},\"message\":\"{businessException.Message}\"}}";
        response.Clear();
        response.StatusCode = (int)httpStatusCode;
        response.ContentType = "application/json";

        response.GetTypedHeaders().CacheControl =
          new CacheControlHeaderValue { NoStore = true, NoCache = true };

        response.WriteAsync(message);
        return Task.FromResult(response.StatusCode);
    }

    private static (HttpStatusCode, EventId) GetResponseCode(string exception)
    {
        return exception switch
        {
            // BadRequest
            nameof(EmailInvalidPatternException) => (HttpStatusCode.BadRequest, LoggingEvents.EmailInvalidPattern),

            nameof(EmailMaxLengthException) => (HttpStatusCode.BadRequest, LoggingEvents.EmailMaxLength),
            nameof(EmailNullOrEmptyException) => (HttpStatusCode.BadRequest, LoggingEvents.EmailNullOrEmpty),
            nameof(MobileNullOrEmptyException) => (HttpStatusCode.BadRequest, LoggingEvents.MobileNullOrEmpty),
            nameof(NoValidCityIdException) => (HttpStatusCode.BadRequest, LoggingEvents.NoValidCityId),
            nameof(UserNameMaxLengthException) => (HttpStatusCode.BadRequest, LoggingEvents.UserNameMaxLength),
            nameof(UserNameNullOrEmptyException) => (HttpStatusCode.BadRequest, LoggingEvents.UserNameNullOrEmpty),
                        

            // Conflict
            nameof (AccountEmailAlreadyExistException) => (HttpStatusCode.Conflict, LoggingEvents.AccountEmailAlreadyExist),

            //Default
            _ => (HttpStatusCode.InternalServerError, LoggingEvents.Unknown)
        };
    }
}
