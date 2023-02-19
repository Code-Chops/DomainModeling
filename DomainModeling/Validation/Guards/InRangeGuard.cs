using CodeChops.DomainModeling.Validation.Guards.Core;

namespace CodeChops.DomainModeling.Validation.Guards;

public record NumberInRangeGuard<TNumber> : NoOutputGuardBase<NumberInRangeGuard<TNumber>, (TNumber Index, TNumber? LowerBound, TNumber? UpperBound), (string Name, TNumber Index, TNumber? LowerBound, TNumber? UpperBound)>,
	IHasExceptionMessage<NumberInRangeGuard<TNumber>, (string Name, TNumber Index, TNumber? LowerBound, TNumber? UpperBound)>, 
	INoOutputGuard<(TNumber Index, TNumber? LowerBound, TNumber? UpperBound)>, 
	IGuard<NumberInRangeGuard<TNumber>, (string Name, TNumber Index, TNumber? LowerBound, TNumber? UpperBound)> 
	where TNumber : struct, INumber<TNumber>
{
	public static string GetExceptionMessage(string objectName, (string Name, TNumber Index, TNumber? LowerBound, TNumber? UpperBound) parameter)
		=> $"{{1}} '{{2}}' is out of range for '{{0}}'{parameter.LowerBound?.Write(" (Minimum: '{3}')")}{parameter.UpperBound?.Write(" (Maximum: '{4}')")}.";
	
	public static bool IsValid((TNumber Index, TNumber? LowerBound, TNumber? UpperBound) input)
		=> (input.LowerBound is null || input.Index >= input.LowerBound) && (input.UpperBound is null || input.Index <= input.UpperBound);
}

public record IndexInRangeGuard<TEnumerable, TElement> : OutputGuardBase<IndexInRangeGuard<TEnumerable, TElement>, (int Index, TEnumerable Enumerable, Func<TEnumerable, int, TElement> ElementGetter), (int Index, int Upperbound), TElement?>, 
	IOutputGuard<(int Index, TEnumerable Enumerable, Func<TEnumerable, int, TElement> ElementGetter), TElement?>, 
	IHasExceptionMessage<IndexInRangeGuard<TEnumerable, TElement>, (int Index, int Upperbound)>, 
	IGuard<IndexInRangeGuard<TEnumerable, TElement>, (int Index, int Upperbound)>
	where TEnumerable : class, IEnumerable<TElement>
{
	public static string GetExceptionMessage(string objectName, (int Index, int Upperbound) parameter)
		=> "Value '{1}' is out of range for '{0}' (Minimum: '0') (Maximum: '{2}')";
	
	public static bool IsValid((int Index, TEnumerable Enumerable, Func<TEnumerable, int, TElement> ElementGetter) input, out TElement? output)
	{
		if (!input.Enumerable.TryGetNonEnumeratedCount(out var count))
			count = input.Enumerable.Count();
		
		if (input.Index < 0 || input.Index > count)
		{
			output = default;
			return false;
		}

		output = input.ElementGetter(input.Enumerable, input.Index);
		return true;
	}
}

public static class InRangeGuardExtensions
{
	public static Validator GuardInRange<TNumber>(this Validator validator, TNumber value, TNumber? minimum, TNumber? maximum, object? errorCode, Exception? innerException = null)
		where TNumber : struct, INumber<TNumber> 
		=> NumberInRangeGuard<TNumber>.Guard(
			validator, 
			input: (value, minimum, maximum), 
			messageParameter: ("Value", value, minimum, maximum), 
			errorCode, 
			innerException);

	public static void GuardLengthInRange(this Validator validator, string value, int minimumLength, int? maximumLength, object? errorCode, Exception? innerException = null)
		=> NumberInRangeGuard<int>.Guard(
			validator, 
			input: (value.Length, minimumLength, maximumLength), 
			messageParameter: ("Length of", value.Length, minimumLength, maximumLength), 
			errorCode, 
			innerException);
	
	public static TElement? GuardIndexInRange<TElement>(this Validator validator, IReadOnlyList<TElement> list, int index, object? errorCode, Exception? innerException = null)
		=> IndexInRangeGuard<IReadOnlyList<TElement>, TElement>.Guard(
			validator, 
			input: (index, list, static (list, index) => list[index]), 
			messageParameter: (index, list.Count), 
			errorCode,
			innerException);
	
	public static char GuardIndexInRange(this Validator validator, string value, int index, object? errorCode, Exception? innerException = null) 
		=> IndexInRangeGuard<string, char>.Guard(
			validator, 
			input: (index, value, static (list, index) => list[index]), 
			messageParameter: (index, value.Length), 
			errorCode,
			innerException);
}
