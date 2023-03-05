namespace CodeChops.DomainModeling.Factories;

/// <summary>
/// Has an immutable instance of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">Type of the immutable default instance</typeparam>
public interface IHasDefault<out T>
{
	static abstract T Default { get; }
}
