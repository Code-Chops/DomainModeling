using System.Globalization;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Attributes;

/// <summary>
/// Generates a value object with a single structural value of type string.
/// </summary>
[AttributeUsage(AttributeTargets.Struct)]
public sealed class GenerateStringValueObject : Attribute
{
	/// <param name="minimumLength">The minimum length of the string. Default: no minimum length.</param>
	/// <param name="maximumLength">The maximum length of the string. Default: no maximum length.</param>
	/// <param name="stringCaseConversion">Converts the case to upper/lower case. Default: no conversion.</param>
	/// <param name="stringFormat">Make the string only accept certain characters. Default: no special format.</param>
	/// <param name="generateEmptyStatic">Generate a static property with a default value. Default: true.</param>
	/// <param name="generateCasts">Generate implicit casts from value object to string and an implicit cast from string to value object. Default: true.</param>
	/// <param name="generateEnumerable">Make the string enumerable. Default: true.</param>
	/// <param name="compareOptions">See <see cref="CompareOptions"/>. Default: default string comparison.</param>
	// ReSharper disable twice UnusedParameter.Local
	public GenerateStringValueObject(
		int minimumLength,
		int maximumLength,
		StringCaseConversion stringCaseConversion,
		StringFormat stringFormat,
		bool generateEmptyStatic,
		bool generateCasts,
		bool generateEnumerable,
		CompareOptions compareOptions)
	{
		// These parameters will be read from the attribute arguments itself and therefore don't need to be assigned.
	}
}

/// <summary>
/// DO NOT RENAME!
/// </summary>
public enum StringCaseConversion
{
	NoConversion,
	LowerInvariant,
	UpperInvariant,
}

public enum StringFormat
{
	Default,
	Alpha,
	AlphaWithUnderscore,
	AlphaNumeric,
	AlphaNumericWithUnderscore,
}