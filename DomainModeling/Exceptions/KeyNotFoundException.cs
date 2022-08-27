namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public class KeyNotFoundException<TKey, TCollection> : SystemException<KeyNotFoundException<TKey, TCollection>>, ISystemException<KeyNotFoundException<TKey, TCollection>>
{
	public static string ErrorMessage { get; } = $"{typeof(TKey).Name} of {typeof(TCollection).Name} not found in {typeof(TCollection).Name}.";

	public static KeyNotFoundException<TKey, TCollection> Create(object parameters)
		=> new(parameters);

	public KeyNotFoundException(object parameters) : base(parameters)
	{
	}
}