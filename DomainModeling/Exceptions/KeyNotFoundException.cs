namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public class KeyNotFoundException<TKey, TCollection> : SystemException<KeyNotFoundException<TKey, TCollection>, TKey>, ISystemException<KeyNotFoundException<TKey, TCollection>, TKey> 
	where TKey : notnull
{
	public static string ErrorMessage { get; } = $"{typeof(TKey).Name} of {typeof(TCollection).Name} not found in {typeof(TCollection).Name}.";

	public static KeyNotFoundException<TKey, TCollection> Create(TKey key)
		=> new(key);

	public KeyNotFoundException(TKey parameter) : base(parameter)
	{
	}
}