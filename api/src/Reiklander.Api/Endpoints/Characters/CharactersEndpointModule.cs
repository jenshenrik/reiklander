using Asp.Versioning;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Reiklander.Api.Endpoints.Characters;

public static class CharactersEndpointModule
{
    public static IEndpointRouteBuilder AddCharacterModule(this IEndpointRouteBuilder app)
    {
        var versionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1, 0))
            .HasApiVersion(new ApiVersion(2, 0))
            .ReportApiVersions()
            .Build();

        var builder = app.MapGroup("api/v{version:apiVersion}/characters")
            .WithTags("Characters")
            .WithApiVersionSet(versionSet);

        builder.MapGet("/", GetAllCharactersAsync)
            .MapToApiVersion(1.0);

        builder.MapGet("/", GetAllCharactersAsync2)
            .MapToApiVersion(2.0);

        return app;
    }

    private static async Task<Ok<string>> GetAllCharactersAsync(CancellationToken cancellationToken)
    {
        return TypedResults.Ok("all chars v1");
    }

    private static async Task<Ok<string>> GetAllCharactersAsync2(CancellationToken cancellationToken)
    {
        return TypedResults.Ok("all chars v2");
    }
}
