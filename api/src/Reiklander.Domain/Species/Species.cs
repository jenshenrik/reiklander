using System.Diagnostics.Contracts;

namespace Reiklander.Domain.Species;

public class Species(string Name/*, List<Skill> skills, List<Talent> talents*/)
{
    public string Name { get; } = Name;
    // public IReadOnlyList<Skill> StartingSkills { get; }
    // public IReadOnlyList<Talent> StartingTalents { get; }
}
