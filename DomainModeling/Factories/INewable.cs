namespace CodeChops.DomainModeling.Factories;

public interface INewable<out TSelf>
	where TSelf : INewable<TSelf>, IDomainObject, new()
{
}
