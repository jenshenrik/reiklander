namespace Reiklander.Infrastructure;

public class CharacterReadModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public int Experience { get; set; }
}
