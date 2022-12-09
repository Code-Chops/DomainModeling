using CodeChops.DomainDrivenDesign.DomainModeling.Validation.Guards.Core;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Validation.Guards;

public record KeyExistsNoOutputGuard<TKey> : NoOutputGuardBase<KeyExistsNoOutputGuard<TKey>, (TKey Key, Func<TKey, bool> Retriever), TKey>, 
	INoOutputGuard<(TKey Key, Func<TKey, bool> Retriever)>, 
	IHasExceptionMessage<KeyExistsNoOutputGuard<TKey>, TKey>, 
	IGuard<KeyExistsNoOutputGuard<TKey>, TKey> 
	where TKey : notnull
{
	public static string GetExceptionMessage(string objectName, TKey key)
		=> "'{0}' '{1}' not found.";

	public static bool IsValid((TKey Key, Func<TKey, bool> Retriever) input)
		=> input.Retriever.Invoke(input.Key);
}

public record KeyExistsGuard<TKey, TValue> : OutputGuardBase<KeyExistsGuard<TKey, TValue>, (TKey Key, Func<TKey, TValue?> Retriever), TKey, TValue>, 
	IOutputGuard<(TKey Key, Func<TKey, TValue?> Retriever), TValue>, 
	IHasExceptionMessage<KeyExistsGuard<TKey, TValue>, TKey>, 
	IGuard<KeyExistsGuard<TKey, TValue>, TKey> 
	where TKey : notnull
{
	public static string GetExceptionMessage(string objectName, TKey key)
		=> KeyExistsNoOutputGuard<TKey>.GetExceptionMessage(objectName, key);

	public static bool IsValid((TKey Key, Func<TKey, TValue?> Retriever) input, out TValue output)
		=> (output = input.Retriever(input.Key)!) is not null && EqualityComparer<TValue>.Default.Equals(output, default);
}

public static class GuardKeyExistsExtensions
{
	public static void GuardKeyExists<TKey>(this Validator validator, Func<TKey, bool> retriever, TKey key, object? errorCode, Exception? innerException = null)
		where TKey : notnull
		=> KeyExistsNoOutputGuard<TKey>.Guard(validator, (key, retriever), messageParameter: key, errorCode, innerException);
	
	public static TValue? GuardKeyExists<TKey, TValue>(this Validator validator, Func<TKey, TValue?> retriever, TKey key, object? errorCode, Exception? innerException = null)
		where TKey : notnull
		=> KeyExistsGuard<TKey, TValue>.Guard(validator, (key, retriever), messageParameter: key, errorCode, innerException);
}
