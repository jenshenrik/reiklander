using Asp.Versioning;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Reiklander.Api.Endpoints.Characters.Contracts;
using Reiklander.Application.Characters.CreateCharacter;

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

        builder.MapPost("/", CreateCharacter)
            .Produces<Guid>(StatusCodes.Status201Created)
            .MapToApiVersion(1.0);

        return app;
    }

    private static async Task<Ok<string>> GetAllCharactersAsync(CancellationToken cancellationToken)
    {
        return TypedResults.Ok("all chars v1");
    }

    private static async Task<Created<Guid>> CreateCharacter([FromBody] CreateCharacterDto request, CreateCharacterHandler handler)
    {
        var id = await handler.Handle(new CreateCharacterCommand(request.Name));

        return TypedResults.Created($"/characters/{id}", id);
    }
}
