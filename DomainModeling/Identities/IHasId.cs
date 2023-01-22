namespace CodeChops.DomainModeling.Identities;

public interface IHasId<out TId>
	where TId : IId
{
	TId Id { get; }
}
