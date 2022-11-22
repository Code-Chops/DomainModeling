using CodeChops.DomainDrivenDesign.DomainModeling.Validation.Guards.Core;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Validation.Guards;

public record InRangeNoOutputGuard<TNumber> : NoOutputGuardBase<InRangeNoOutputGuard<TNumber>, (Number<TNumber> Index, Number<TNumber>? LowerBound, Number<TNumber>? UpperBound)>,
	IHasExceptionMessage<InRangeNoOutputGuard<TNumber>, (Number<TNumber> Index, Number<TNumber>? LowerBound, Number<TNumber>? UpperBound)>, 
	INoOutputGuard<(Number<TNumber> Index, Number<TNumber>? LowerBound, Number<TNumber>? UpperBound)>, 
	IGuard<InRangeNoOutputGuard<TNumber>, (Number<TNumber> Index, Number<TNumber>? LowerBound, Number<TNumber>? UpperBound)> 
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public static string GetMessage(string objectName, (Number<TNumber> Index, Number<TNumber>? LowerBound, Number<TNumber>? UpperBound) parameter)
		=> $"Value {{1}} is out of range for {{0}} {parameter.LowerBound?.Write(" (Lower bound: {2})")}{parameter.UpperBound?.Write(" (Upper bound: {3})")}.";
	
	public static bool IsValid((Number<TNumber> Index, Number<TNumber>? LowerBound, Number<TNumber>? UpperBound) input)
		=> input.Index >= input.LowerBound && input.Index <= input.UpperBound;
}

public record InRangeGuard<TElement> : OutputGuardBase<InRangeGuard<TElement>, (IReadOnlyList<TElement> List, int Index), TElement?, (int Index, int Upperbound)>, 
	IOutputGuard<(IReadOnlyList<TElement> List, int Index), TElement?>, 
	IHasExceptionMessage<InRangeGuard<TElement>, (int Index, int Upperbound)>, 
	IGuard<InRangeGuard<TElement>, (int Index, int Upperbound)>
{
	public static string GetMessage(string objectName, (int Index, int Upperbound) parameter)
		=> $"Value {{1}} is out of range for {{0}} (Lower bound: 0) (Upper bound: {parameter.Upperbound})";
	
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
	
	public static TElement? GuardInRange<TElement>(this Validator validator, IReadOnlyList<TElement> list, int index, IErrorCode? errorCode, Exception? innerException = null)
		=> InRangeGuard<TElement>.Guard(validator, (list, index), (index, list.Count), errorCode, innerException);
}
