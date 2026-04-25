namespace Reiklander.Domain.Characters;

public class AttributeState
{
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


