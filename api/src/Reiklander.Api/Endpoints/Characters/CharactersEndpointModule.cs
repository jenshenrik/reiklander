using Asp.Versioning;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Reiklander.Api.Endpoints.Characters.Contracts;
using Reiklander.Application;
using Reiklander.Application.Characters.CreateCharacter;
using Reiklander.Application.Characters.EarnExperiencePoints;
using Reiklander.Application.Characters.AdvanceAttribute;
using Reiklander.Contracts.Characters;
using Reiklander.Domain.Characters;
using Reiklander.Infrastructure;

namespace Reiklander.Api.Endpoints.Characters;

public static class CharactersEndpointModule
{
    public static IEndpointRouteBuilder AddCharacterModule(this IEndpointRouteBuilder app)
    {
        var versionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1, 0))
            .ReportApiVersions()
            .Build();

        var builder = app.MapGroup("api/v{version:apiVersion}/characters")
            .WithTags("Characters")
            .WithApiVersionSet(versionSet);

        builder.MapGet("/", GetAllCharactersAsync)
            .MapToApiVersion(1.0);

        builder.MapGet("/{id}", GetCharacterAsync)
            .WithTags("Get character")
            .Produces(StatusCodes.Status404NotFound)
            .Produces<CharacterResponse>(StatusCodes.Status200OK);

        builder.MapPost("/", CreateCharacterAsync)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .MapToApiVersion(1.0);

        builder.MapPost("/{id}/xp", EarnExperiencePoints)
            .WithTags("Earn experience points")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .MapToApiVersion(1.0);

        builder.MapPost("/{id}/{attribute}/advance", AdvanceAttribute)
            .WithTags("Advance attribute")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .MapToApiVersion(1.0);

        return app;
    }

    private static async Task<Ok<string>> GetAllCharactersAsync(CancellationToken cancellationToken)
    {
        return TypedResults.Ok("all chars v1");
    }

    private static async Task<IResult> GetCharacterAsync([FromRoute] Guid id, ICharacterQueries queries)
    {
        var character = await queries.GetById(id);

        if (character == null)
            return Results.NotFound();

        return TypedResults.Ok(character);
    }

    private static async Task<IResult> CreateCharacterAsync([FromBody] CreateCharacterRequest request, CreateCharacterHandler handler)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return Results.BadRequest("Name cannot be empty.");

        var id = await handler.Handle(new CreateCharacterCommand(request.Name));

        return TypedResults.Created($"/characters/{id}", id);
    }

    private static async Task<IResult> EarnExperiencePoints([FromRoute] Guid id, [FromBody] EarhXpRequest request, EarnExperiencePointsHandler handler)
    {
        await handler.Handle(new EarnExperiencePointsCommand(id, request.Amount));

        return TypedResults.Ok();
    }

    private static async Task<IResult> AdvanceAttribute([FromRoute] Guid id, [FromRoute] AttributeType attribute, AdvanceAttributeHandler handler)
    {
        await handler.Handle(new AdvanceAttributeCommand(id, attribute));

        return TypedResults.Ok();
    }
}
