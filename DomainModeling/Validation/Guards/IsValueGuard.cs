using CodeChops.DomainModeling.Validation.Guards.Core;

namespace CodeChops.DomainModeling.Validation.Guards;

public record IsValueGuard<TValue> : NoOutputGuardBase<IsValueGuard<TValue>, (TValue CurrentValue, TValue ExpectedValue)>,
	INoOutputGuard<(TValue CurrentValue, TValue ExpectedValue)>,
	IHasExceptionMessage<IsValueGuard<TValue>, (TValue CurrentValue, TValue ExpectedValue)>,
	IGuard<IsValueGuard<TValue>, (TValue CurrentValue, TValue ExpectedValue)>
{
	public static string GetExceptionMessage(string objectName, (TValue CurrentValue, TValue ExpectedValue) parameter)
		=> "Value '{1}' of '{0}' should be '{2}'.";

	public static bool IsValid((TValue CurrentValue, TValue ExpectedValue) input)
		=> input.CurrentValue?.Equals(input.ExpectedValue) ?? input.ExpectedValue is null;
}

public static class IsValueGuardExtensions
{
	public static Validator GuardIsValue<TValue>(this Validator validator, TValue currentValue, TValue expectedValue, object? errorCode, Exception? innerException = null)
		=> IsValueGuard<TValue>.Guard(validator, (currentValue, expectedValue), (currentValue, expectedValue), errorCode, innerException);
}
