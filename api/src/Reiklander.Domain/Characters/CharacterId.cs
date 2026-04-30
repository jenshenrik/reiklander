using Reiklander.Domain.Kernel;

namespace Reiklander.Domain.Characters;

public readonly record struct CharacterId(Guid Value) : IAggregateId<CharacterId, Guid>
{
    public static CharacterId Create() => new(Guid.NewGuid());
    public static CharacterId From(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("Character Id is empty");
        return new(value);
    }

    public override string ToString() => Value.ToString();
}
