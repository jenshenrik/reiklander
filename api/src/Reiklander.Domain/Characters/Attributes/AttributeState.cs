namespace Reiklander.Domain.Characters.Attributes;

public class AttributeState
{
    public static AttributeState From(int value, int advances) =>
        new()
        { Value = value, Advances = advances };

    public int Value { get; private set; }
    public int Advances { get; private set; }

    public int Bonus => Value / 10;

    public void Advance()
    {
        Value++;
        Advances++;
    }

    public int GetAdvanceCost()
    {
        // TODO: implement
        return 50;
    }

    public void Load(int value, int advances)
    {
        Value = value;
        Advances = advances;
    }
}


