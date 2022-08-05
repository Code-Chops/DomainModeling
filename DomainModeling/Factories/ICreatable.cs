namespace CodeChops.DomainDrivenDesign.DomainModeling.Factories;

/// <summary>
/// Has a factory to create objects of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type to be created.</typeparam>
public interface ICreatable<out T>
{
	static abstract T Create();
}