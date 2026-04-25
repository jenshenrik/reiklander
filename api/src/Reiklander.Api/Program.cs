using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Reiklander.Api.Endpoints.Characters;
using Reiklander.Application;
using Reiklander.Application.Characters.CreateCharacter;
using Reiklander.Application.Characters.EarnExperiencePoints;
using Reiklander.Domain.Kernel;
using Reiklander.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EventStoreDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();

builder.Services.AddScoped<CreateCharacterHandler>();
builder.Services.AddScoped<EarnExperiencePointsHandler>();

builder.Services.AddScoped<ICharacterQueries, CharacterQueries>();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi("v1", options =>
{
    options.AddDocumentTransformer((doc, _, _) =>
    {
        doc.Info = new OpenApiInfo
        {
            Title = "Reiklander API",
            Version = "1.0",
            Description = "API for Reiklander, a digital character sheet for WFRP 4th Edition."
        };
        return Task.CompletedTask;
    });
});

var app = builder.Build();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/openapi/v1.json");
    app.MapScalarApiReference();
}

app.AddCharacterModule();

app.Run();
