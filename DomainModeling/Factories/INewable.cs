namespace CodeChops.DomainDrivenDesign.DomainModeling.Factories;

public interface INewable<out TSelf>
	where TSelf : ICreatable<TSelf>, IDomainObject, new()
{
}
