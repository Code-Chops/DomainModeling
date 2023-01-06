using CodeChops.DomainModeling.Validation.Guards.Core;

namespace CodeChops.DomainModeling.Validation.Guards;

public record NotNullGuard<TValue> : NoOutputGuardBase<NotNullGuard<TValue?>, TValue?, string>, 
	INoOutputGuard<TValue>, 
	IHasExceptionMessage<NotNullGuard<TValue?>, string>, 
	IGuard<NotNullGuard<TValue?>, string>
{
	public static string GetExceptionMessage(string objectName, string parameterName)
		=> "Required data '{1}' for '{0}' is missing.";
	
	public static bool IsValid(TValue input)
		=> input is not null;
}

public static class NotNullGuardExtensions
{
	public static void GuardNotNull<TValue>(this Validator validator, TValue value, object? errorCode, Exception? innerException = null)
		=> NotNullGuard<TValue>.Guard(validator, value, typeof(TValue).Name, errorCode, innerException);
}
