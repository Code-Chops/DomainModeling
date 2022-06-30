namespace CodeChops.DomainDrivenDesign.DomainModeling;

public interface IAggregateRoot<TSelf> : IEntity
	where TSelf : IAggregateRoot<TSelf>
{
}