using System.Diagnostics.Contracts;

namespace Reiklander.Domain.Species;

public class Species(string Name, string Identifier/*, List<Skill> skills, List<Talent> talents*/)
{
    public string Name { get; } = Name;
    public string Identifier { get; } = Identifier;
    // public IReadOnlyList<Skill> StartingSkills { get; }
    // public IReadOnlyList<Talent> StartingTalents { get; }
}
