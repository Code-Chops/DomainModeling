using CodeChops.DomainDrivenDesign.DomainModeling.Validation.Guards.Core;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Validation.Guards;

public record InRangeNoOutputGuard<TNumber> : NoOutputGuardBase<InRangeNoOutputGuard<TNumber>, (Number<TNumber> Index, Number<TNumber>? LowerBound, Number<TNumber>? UpperBound)>,
	IHasExceptionMessage<InRangeNoOutputGuard<TNumber>, (Number<TNumber> Index, Number<TNumber>? LowerBound, Number<TNumber>? UpperBound)>, 
	INoOutputGuard<(Number<TNumber> Index, Number<TNumber>? LowerBound, Number<TNumber>? UpperBound)>, 
	IGuard<InRangeNoOutputGuard<TNumber>, (Number<TNumber> Index, Number<TNumber>? LowerBound, Number<TNumber>? UpperBound)> 
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public static string GetExceptionMessage(string objectName, (Number<TNumber> Index, Number<TNumber>? LowerBound, Number<TNumber>? UpperBound) parameter)
		=> $"Value '{{1}}' is out of range for '{{0}}' {parameter.LowerBound?.Write(" (Lower bound: '{2}')")}{parameter.UpperBound?.Write(" (Upper bound: '{3}')")}.";
	
	public static bool IsValid((Number<TNumber> Index, Number<TNumber>? LowerBound, Number<TNumber>? UpperBound) input)
		=> input.Index >= input.LowerBound && input.Index <= input.UpperBound;
}

public record InRangeGuard<TElement> : OutputGuardBase<InRangeGuard<TElement>, (IReadOnlyList<TElement> List, int Index), (int Index, int Upperbound), TElement?>, 
	IOutputGuard<(IReadOnlyList<TElement> List, int Index), TElement?>, 
	IHasExceptionMessage<InRangeGuard<TElement>, (int Index, int Upperbound)>, 
	IGuard<InRangeGuard<TElement>, (int Index, int Upperbound)>
{
	public static string GetExceptionMessage(string objectName, (int Index, int Upperbound) parameter)
		=> $"Value '{{1}}' is out of range for '{{0}}' (Lower bound: '0') (Upper bound: '{parameter.Upperbound}')";
	
	public static bool IsValid((IReadOnlyList<TElement> List, int Index) input, out TElement? output)
	{
		if (input.Index < 0 || input.Index > input.List.Count)
		{
			output = default;
			return false;
		}

		output = input.List[input.Index];
		return true;
	}
}

public record InRangeGuard : NoOutputGuardBase<InRangeGuard, (string Value, int? MinimumLength, int? MaximumLength)>,
	IHasExceptionMessage<InRangeGuard, (string Value, int? MinimumLength, int? MaximumLength)>, 
	INoOutputGuard<(string Value, int? MinimumLength, int? MaximumLength)>, 
	IGuard<InRangeGuard, (string Value, int? MinimumLength, int? MaximumLength)> 
{
	public static string GetExceptionMessage(string objectName, (string Value, int? MinimumLength, int? MaximumLength) parameter)
		=> $"Length is out of range for '{{0}}' '{{1}}' (Lower bound: '{2}') (Upper bound: '{3}').";
	
	public static bool IsValid((string Value, int? MinimumLength, int? MaximumLength) input)
		=> input.Value.Length >= input.MinimumLength && input.Value.Length <= input.MaximumLength;
}

public static class InRangeGuardExtensions
{
	public static void GuardInRange<TNumber>(this Validator validator, Number<TNumber>? index, Number<TNumber>? lowerBound, Number<TNumber>? upperBound, 
		object? errorCode, Exception? innerException = null)
		where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
	{
		if (index is null)
			return;
		
		var parameters = (value: index.Value, lowerBound, upperBound);
		
		InRangeNoOutputGuard<TNumber>.Guard(validator, parameters, parameters, errorCode, innerException);
	}

	public static TElement? GuardInRange<TElement>(this Validator validator, IReadOnlyList<TElement> list, int index, object? errorCode, Exception? innerException = null)
		=> InRangeGuard<TElement>.Guard(validator, (list, index), (index, list.Count), errorCode, innerException);
	
	public static char? GuardInRange(this Validator validator, string? value, int index, object? errorCode, Exception? innerException = null)
	{
		if (value is null)
			return null;
		
		var parameters = (value, 0, value.Length);
		
		InRangeGuard.Guard(validator, parameters, parameters, errorCode, innerException);

		return validator.IsValid
			? value[index]
			: default!;
	}
	
	public static void GuardLengthInRange(this Validator validator, string? value, int minimumLength, int? maximumLength, object? errorCode, Exception? innerException = null)
	{
		if (value is null)
			return;
		
		var parameters = (value, lowerBound: minimumLength, upperBound: maximumLength);
		
		InRangeGuard.Guard(validator, parameters, parameters, errorCode, innerException);
	}
}
