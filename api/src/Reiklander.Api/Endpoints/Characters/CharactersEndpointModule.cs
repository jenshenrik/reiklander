using Asp.Versioning;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Reiklander.Api.Endpoints.Characters.Contracts;
using Reiklander.Application;
using Reiklander.Application.Characters.CreateCharacter;
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

        return Results.Ok(character);
    }

    private static async Task<IResult> CreateCharacterAsync([FromBody] CreateCharacterRequest request, CreateCharacterHandler handler)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return Results.BadRequest("Name cannot be empty.");

        var id = await handler.Handle(new CreateCharacterCommand(request.Name));

        return TypedResults.Created($"/characters/{id}", id);
    }
}
