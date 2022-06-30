using CodeChops.Identities;

namespace CodeChops.DomainDrivenDesign.DomainModeling;

public interface IAggregateRootContract
{
	IId AggregateRootId { get; }
}