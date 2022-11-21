namespace CodeChops.DomainDrivenDesign.DomainModeling.Factories;

/// <summary>
/// Has a value object (immutable) instance of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">Type of the immutable default instance</typeparam>
public interface IHasDefault<out T>
	where T : IValueObject
{
	static abstract T Default { get; }
}
