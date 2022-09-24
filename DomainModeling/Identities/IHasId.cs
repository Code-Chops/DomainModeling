namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

public interface IHasId<out TId>
	where TId : IId
{
	TId Id { get; }
}

public interface IHasId
{
	IId Id { get; }
}