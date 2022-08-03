namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

public interface IHasId<out TId>
	where TId : Id
{
	TId Id { get; }
}

public interface IHasId
{
	Id Id { get; }
}