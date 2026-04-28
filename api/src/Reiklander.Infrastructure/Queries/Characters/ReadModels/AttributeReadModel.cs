namespace Reiklander.Infrastructure.Queries.Characters.ReadModels;

public class CharacteristicReadModel
{
    public CharacteristicReadModel()
    {
        CostToAdvance = 25;
    }

    public int Value { get; set; }
    public int Bonus { get; set; }
    public int CostToAdvance { get; set; }
    public int Advances { get; set; }
}
