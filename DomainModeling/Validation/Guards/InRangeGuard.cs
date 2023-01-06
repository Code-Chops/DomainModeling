using CodeChops.DomainModeling.Validation.Guards.Core;

namespace CodeChops.DomainModeling.Validation.Guards;

public record InRangeNoOutputGuard<TNumber> : NoOutputGuardBase<InRangeNoOutputGuard<TNumber>, (TNumber Index, TNumber? LowerBound, TNumber? UpperBound)>,
	IHasExceptionMessage<InRangeNoOutputGuard<TNumber>, (TNumber Index, TNumber? LowerBound, TNumber? UpperBound)>, 
	INoOutputGuard<(TNumber Index, TNumber? LowerBound, TNumber? UpperBound)>, 
	IGuard<InRangeNoOutputGuard<TNumber>, (TNumber Index, TNumber? LowerBound, TNumber? UpperBound)> 
	where TNumber : struct, INumber<TNumber>
{
	public static string GetExceptionMessage(string objectName, (TNumber Index, TNumber? LowerBound, TNumber? UpperBound) parameter)
		=> $"Value '{{1}}' is out of range for '{{0}}' {parameter.LowerBound?.Write(" (Lower bound: '{2}')")}{parameter.UpperBound?.Write(" (Upper bound: '{3}')")}.";
	
	public static bool IsValid((TNumber Index, TNumber? LowerBound, TNumber? UpperBound) input)
		=> (input.LowerBound is null || input.Index >= input.LowerBound) && (input.UpperBound is null || input.Index <= input.UpperBound);
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
	public static void GuardInRange<TNumber>(this Validator validator, TNumber index, TNumber? lowerBound, TNumber? upperBound, 
		object? errorCode, Exception? innerException = null)
		where TNumber : struct, INumber<TNumber> 
		=> InRangeNoOutputGuard<TNumber>.Guard(validator, input: (index, lowerBound, upperBound), messageParameter: (index, lowerBound, upperBound), errorCode, innerException);

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
