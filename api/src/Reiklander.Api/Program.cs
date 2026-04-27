using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Reiklander.Api.Endpoints.Characters;
using Reiklander.Application;
using Reiklander.Application.Characters.AdvanceAttribute;
using Reiklander.Application.Characters.CreateCharacter;
using Reiklander.Application.Characters.EarnExperiencePoints;
using Reiklander.Application.Characters.NameCharacter;
using Reiklander.Application.Characters.SelectSpecies;
using Reiklander.Application.Kernel;
using Reiklander.Domain.Characters.Events;
using Reiklander.Domain.Kernel;
using Reiklander.Infrastructure.Persistence;
using Reiklander.Infrastructure.Projections;
using Reiklander.Infrastructure.Projections.Characters;
using Reiklander.Infrastructure.Queries.Characters;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EventStoreDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddScoped<ISpeciesRepository, SpeciesRepository>();

builder.Services.AddScoped<CreateCharacterHandler>();
builder.Services.AddScoped<SelectSpeciesHandler>();
builder.Services.AddScoped<NameCharacterHandler>();
builder.Services.AddScoped<EarnExperiencePointsHandler>();
builder.Services.AddScoped<AdvanceAttributeHandler>();

builder.Services.AddScoped<ICharacterQueries, CharacterQueries>();

builder.Services.AddScoped<ProjectionDispatcher>();
builder.Services.AddScoped<IProjectionHandler<CharacterCreated>, CharacterCreatedProjection>();
builder.Services.AddScoped<IProjectionHandler<SpeciesSelected>, SpeciesSelectedProjection>();
builder.Services.AddScoped<IProjectionHandler<NameCharacter>, NameCharacterProjection>();
builder.Services.AddScoped<IProjectionHandler<ExperienceEarned>, ExperienceEarnedProjection>();
builder.Services.AddScoped<IProjectionHandler<ExperienceSpent>, ExperienceSpentProjection>();
builder.Services.AddScoped<IProjectionHandler<AttributeAdvanced>, AttributeAdvancedProjection>();

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
