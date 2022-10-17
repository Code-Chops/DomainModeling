namespace CodeChops.DomainDrivenDesign.DomainModeling.Factories;

/// <summary>
/// Has a value object instance of type <typeparamref name="T"/>. Be sure that the default instance is immutable!
/// </summary>
/// <typeparam name="T">Type of the immutable default instance</typeparam>
public interface IHasDefaultInstance<out T>
	where T : IValueObject
{
	static abstract T Default { get; }
}