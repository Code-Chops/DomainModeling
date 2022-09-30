namespace CodeChops.DomainDrivenDesign.DomainModeling.Factories;

/// <summary>
/// Has a value object instance of type <typeparamref name="T"/>. Be sure that the empty instance is immutable!
/// </summary>
/// <typeparam name="T">Type of the immutable empty instance</typeparam>
public interface IHasEmptyInstance<out T>
	where T : IValueObject
{
	static abstract T Empty { get; }
}