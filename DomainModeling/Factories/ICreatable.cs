namespace CodeChops.DomainDrivenDesign.DomainModeling.Factories;

/// <summary>
/// Has a factory to create objects of type <typeparamref name="T"/> with parameter <typeparamref name="TParameter"/>.
/// </summary>
/// <typeparam name="T">The type to be created.</typeparam>
public interface ICreatable<out T, in TParameter>
{
	static abstract T Create(TParameter parameter);
}