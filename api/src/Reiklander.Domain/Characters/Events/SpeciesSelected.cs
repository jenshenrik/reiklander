using Reiklander.Domain.Kernel;

namespace Reiklander.Domain.Characters.Events;

public record SpeciesSelected(string Species) : IDomainEvent { };
