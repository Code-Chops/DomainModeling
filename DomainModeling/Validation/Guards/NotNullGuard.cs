using CodeChops.DomainDrivenDesign.DomainModeling.Validation.Guards.Core;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Validation.Guards;

public record NotNullGuard<TValue> : NoOutputGuardBase<NotNullGuard<TValue?>, TValue?, string>, 
	INoOutputGuard<TValue>, 
	IHasExceptionMessage<NotNullGuard<TValue?>, string>, IGuard<NotNullGuard<TValue?>, string>
{
	public static string GetMessage(string objectName, string parameterName)
		=> "Required data {1} for {0} is missing.";
	
	public static bool IsValid(TValue input)
		=> input is not null;
}

public static class NotNullGuardExtensions
{
	public static Validator GuardNotNull<TValue>(this Validator validator, TValue value, IErrorCode? errorCode, Exception? innerException = null)
		=> NotNullGuard<TValue>.Guard(validator, value, typeof(TValue).Name, errorCode, innerException);
}
