using Asp.Versioning;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Reiklander.Application;
using Reiklander.Application.Characters.CreateCharacter;
using Reiklander.Application.Characters.NameCharacter;
using Reiklander.Application.Characters.EarnExperiencePoints;
using Reiklander.Contracts.Characters;
using Reiklander.Application.Characters.SelectSpecies;
using Reiklander.Domain.Characters.Characteristics;
using Reiklander.Application.Characters.AdvanceCharacteristic;

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
            .WithTags("Get all characters")
            .MapToApiVersion(1.0);

        builder.MapGet("/{id}", GetCharacterAsync)
            .WithTags("Get character")
            .Produces(StatusCodes.Status404NotFound)
            .Produces<CharacterResponse>(StatusCodes.Status200OK);

        builder.MapPost("/", CreateCharacterAsync)
            .WithTags("Create new character")
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .MapToApiVersion(1.0);

        builder.MapPost("/{id}/species", SelectSpeciesAsync)
            .WithTags("Select species")
            .Produces(StatusCodes.Status102Processing)
            .Produces(StatusCodes.Status400BadRequest)
            .MapToApiVersion(1.0);

        builder.MapPost("/{id}/xp", EarnExperiencePoints)
            .WithTags("Earn experience points")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .MapToApiVersion(1.0);

        builder.MapPost("/{id}/characteristics/{characteristic}/advance", AdvanceCharacteristic)
            .WithTags("Advance characteristic")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .MapToApiVersion(1.0);

        builder.MapPost("/{id}/name", NameCharacter)
            .WithTags("Name character")
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

    private static async Task<IResult> CreateCharacterAsync(CreateCharacterHandler handler)
    {
        var id = await handler.Handle(new CreateCharacterCommand());

        return TypedResults.Created($"/characters/{id}", id);
    }

    private static async Task<IResult> SelectSpeciesAsync(Guid id, [FromBody] SelectSpeciesRequest request, SelectSpeciesHandler handler)
    {
        await handler.Handle(new SelectSpeciesCommand(id, request.SpeciesName));

        return TypedResults.Ok();
    }

    private static async Task<IResult> NameCharacter([FromRoute] Guid id, [FromBody] NameCharacterRequest request, NameCharacterHandler handler)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return Results.BadRequest("Name cannot be empty.");

        Console.WriteLine($"{id}");

        await handler.Handle(new NameCharacterCommand(id, request.Name));

        return TypedResults.Ok();
    }

    private static async Task<IResult> EarnExperiencePoints([FromRoute] Guid id, [FromBody] EarnXpRequest request, EarnExperiencePointsHandler handler)
    {
        await handler.Handle(new EarnExperiencePointsCommand(id, request.Amount));

        return TypedResults.Ok();
    }

    private static async Task<IResult> AdvanceCharacteristic([FromRoute] Guid id, [FromRoute] CharacteristicType characteristic, AdvanceCharacteristicHandler handler)
    {
        await handler.Handle(new AdvanceCharacteristicCommand(id, characteristic));

        return TypedResults.Ok();
    }
}
