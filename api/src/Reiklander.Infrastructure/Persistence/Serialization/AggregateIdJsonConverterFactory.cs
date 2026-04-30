using System.Text.Json;
using System.Text.Json.Serialization;
using Reiklander.Domain.Kernel;

namespace Reiklander.Infrastructure.Persistence.Serialization;

public sealed class AggregateIdJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        if (!typeToConvert.IsValueType) return false;
        if (Nullable.GetUnderlyingType(typeToConvert) is not null) return false;
        return typeToConvert.GetInterfaces().Any(IsClosedSelfTypedAggregateId);
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var iface = typeToConvert.GetInterfaces().First(IsClosedSelfTypedAggregateId);
        var primitiveType = iface.GetGenericArguments()[1];

        var converterType = typeof(AggregateIdJsonConverter<,>)
            .MakeGenericType(typeToConvert, primitiveType);

        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }

    private static bool IsClosedSelfTypedAggregateId(Type t) =>
        t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IAggregateId<,>);
}

internal sealed class AggregateIdJsonConverter<TId, TPrimitive> : JsonConverter<TId>
    where TId : struct, IAggregateId<TId, TPrimitive>
    where TPrimitive : notnull
{
    public override TId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var primitive = JsonSerializer.Deserialize<TPrimitive>(ref reader, options)
            ?? throw new JsonException($"Cannot deserialize null primitive into {typeToConvert.Name}");

        return TId.From(primitive);
    }

    public override void Write(Utf8JsonWriter writer, TId value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.Value, options);
    }
}
