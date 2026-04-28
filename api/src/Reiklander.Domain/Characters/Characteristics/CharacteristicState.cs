namespace Reiklander.Domain.Characters.Characteristics;

public record CharacteristicState(int value, int advances)
{
    public int Value { get; private set; } = value;
    public int Advances { get; private set; } = advances;

    public int Bonus => Value / 10;

    public static CharacteristicState From(int value, int advances) =>
        new(value, advances);

    public CharacteristicState Advance() => new CharacteristicState(Value + 1, Advances + 1);

    public int GetAdvanceCost()
    {
        // TODO: implement
        return 50;
    }
}


