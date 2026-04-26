namespace Reiklander.Domain.Characters.Attributes;

public record AttributeState(int value, int advances)
{
    public int Value { get; private set; } = value;
    public int Advances { get; private set; } = advances;

    public int Bonus => Value / 10;

    public static AttributeState From(int value, int advances) =>
        new(value, advances);

    public AttributeState Advance() => new AttributeState(Value + 1, Advances + 1);

    public int GetAdvanceCost()
    {
        // TODO: implement
        return 50;
    }
}


