using CodeChops.DomainDrivenDesign.DomainModeling.Validation.Guards.Core;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Validation.Guards;

public record InRangeNoOutputGuard<TNumber> : NoOutputGuardBase<InRangeNoOutputGuard<TNumber>, (Number<TNumber> Index, Number<TNumber>? LowerBound, Number<TNumber>? UpperBound)>,
	IHasExceptionMessage<InRangeNoOutputGuard<TNumber>, (Number<TNumber> Index, Number<TNumber>? LowerBound, Number<TNumber>? UpperBound)>, 
	INoOutputGuard<(Number<TNumber> Index, Number<TNumber>? LowerBound, Number<TNumber>? UpperBound)>, IGuard<InRangeNoOutputGuard<TNumber>, (Number<TNumber> Index, Number<TNumber>? LowerBound, Number<TNumber>? UpperBound)> 
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public static string GetMessage(string objectName, (Number<TNumber> Index, Number<TNumber>? LowerBound, Number<TNumber>? UpperBound) parameter)
		=> $"{{0}} {{1}} is out of range{parameter.LowerBound?.Write(" (Lower bound: {2})")}{parameter.UpperBound?.Write(" (Upper bound: {3})")}.";
	
	public static bool IsValid((Number<TNumber> Index, Number<TNumber>? LowerBound, Number<TNumber>? UpperBound) input)
		=> input.Index >= input.LowerBound && input.Index <= input.UpperBound;
}

public static class InRangeGuardExtensions
{
	public static Validator GuardInRange<TNumber>(this Validator validator, Number<TNumber>? index, Number<TNumber>? lowerBound, Number<TNumber>? upperBound, 
		IErrorCode? errorCode, Exception? innerException = null)
		where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
	{
		if (index is null)
			return validator;
		
		var parameters = (value: index.Value, lowerBound, upperBound);
		
		return InRangeNoOutputGuard<TNumber>.Guard(validator, parameters, parameters, errorCode, innerException);
	}

	public static char? GuardInRange(this Validator validator, string? value, int index, IErrorCode? errorCode, Exception? innerException = null)
	{
		if (value is null)
			return null;
		
		var parameters = (index, 0, value.Length);
		
		validator = InRangeNoOutputGuard<int>.Guard(validator, parameters, parameters, errorCode, innerException);

		return validator.IsValid
			? value[index]
			: default!;
	}
	
	public static TElement? GuardInRange<TElement>(this Validator validator, IReadOnlyList<TElement> enumerable, int index, IErrorCode? errorCode, Exception? innerException = null)
	{
		var parameters = (index, 0, enumerable.Count);
		validator = InRangeNoOutputGuard<int>.Guard(validator, parameters, parameters, errorCode, innerException);

		return validator.IsValid
			? enumerable[index]
			: default;
	}
}
