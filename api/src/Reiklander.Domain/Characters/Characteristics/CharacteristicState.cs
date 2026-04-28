namespace Reiklander.Domain.Characters.Characteristics;

public record CharacteristicState(int Value, int Advances)
{
    public int Bonus => Value / 10;

    public int AdvanceCost =>
        Advances switch
        {
            < 0 => throw new InvalidDataException("Characteristics cannot have negative advances"),
            >= 0 and <= 5 => 25,
            >= 6 and <= 10 => 30,
            >= 11 and <= 15 => 40,
            >= 16 and <= 20 => 50,
            >= 21 and <= 25 => 70,
            >= 26 and <= 30 => 90,
            >= 31 and <= 35 => 120,
            >= 36 and <= 40 => 150,
            >= 41 and <= 45 => 190,
            >= 46 and <= 50 => 230,
            > 50 => throw new InvalidOperationException("Characteristic cannot be advanced further")
        };

    public CharacteristicState Advance() => this with { Value = Value + 1, Advances = Advances + 1 };

    public static CharacteristicState From(int value, int advances) =>
        new(value, advances);
}


