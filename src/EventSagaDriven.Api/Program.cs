using EventSagaDriven.Api.Middleware;
using EventSagaDriven.Application.DependencyInjection;
using EventSagaDriven.Infrastructure.DependencyInjection;
using EventSagaDriven.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.Net.Mime;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var problemDetails = CustomBadRequest.ConstructErrorMessages(context);

        var result = new BadRequestObjectResult(problemDetails);
        result.ContentTypes.Add(MediaTypeNames.Application.Json);

        return result;
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "DDD-Hexagonal-CQRS-Event-Driven API",
        Description = "A Book Library Account Microservice built using C#. It leverages several advanced software design principles and architectures to ensure maintainability, scalability, and testability.",
        Contact = new OpenApiContact
        {
            Name = "Ever Echeverri LinkedIn",
            Email = "everecheverri@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/ever-alonso-echeverri-velasquez-39956414a/")
        },
        License = new OpenApiLicense
        {
            Name = "Github",
            Url = new Uri("https://github.com/EverEcheverri")
        }
    });
});


builder.Services.AddDbContext<CosmosEventSagaDrivenContext>();
builder.Services.AddDbContext<SqliteEventSagaDrivenContext>();

builder.Services.AddUseCases();

builder.Services.AddRepositories(builder.Configuration);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddEventHandlersUseCases();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

bool useCosmos = builder.Configuration.GetValue<bool>("Databases:Cosmos");

if (useCosmos)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<CosmosEventSagaDrivenContext>();
    context.Database.EnsureCreated();
}

using (var scope = app.Services.CreateScope())
{
    var sqliteContext = scope.ServiceProvider.GetRequiredService<SqliteEventSagaDrivenContext>();
    sqliteContext.Database.Migrate();
}

app.UseCustomMiddleware();
app.UseExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
