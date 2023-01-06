namespace CodeChops.DomainModeling.Attributes.ValueObjects;

#pragma warning disable IDE0060 // Remove unused parameter

/// <summary>
/// <para>Generates a value object with a string as single structural value.</para>
/// <para>Any manually extra added property is not being used for calculating Equals or GetHashCode.</para>
/// </summary>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
public sealed class GenerateStringValueObjectAttribute : Attribute
{
	/// <param name="minimumLength">The minimum length of the string.</param>
	/// <param name="maximumLength">The maximum length of the string.</param>
	/// <param name="useRegex">Uses a static regex method to (in)validate.</param>
	/// <param name="stringFormat">Make the string only accept certain characters.</param>
	/// <param name="stringCaseConversion">Converts the case to upper/lower case. Default: NoConversion.</param>
	/// <param name="generateEnumerable">Generates an IEnumerable implementation and indexer. Default: true.</param>
	/// <param name="generateToString">Generates a ToString(). Default: true.</param>
	/// <param name="generateComparison">Generates comparison operators and CompareTo. Default: true.</param>
	/// <param name="generateDefaultConstructor">Set to true if a default constructor should be generated. If it is false, no default constructor will be generated. Default: true.</param>
	/// <param name="forbidParameterlessConstruction">If true (default), it creates an obsolete parameterless private constructor that throws an exception.</param>
	/// <param name="generateStaticDefault">Generate a static property with a default value. Default: false.</param>
	/// <param name="propertyName">The name of the property. Default: 'Value'.</param>
	/// <param name="propertyIsPublic">If true, the generated property will be publicly accessible (not settable). Default: false.</param>
	/// <param name="valueIsNullable">Allow the string to be null. Default: false.</param>
	/// <param name="useValidationExceptions">Throw validation exceptions instead of system exceptions. Default: true.</param>
	// ReSharper disable always UnusedParameter.Local
	public GenerateStringValueObjectAttribute(
		int minimumLength,
		int maximumLength,
		bool useRegex, 
		StringFormat stringFormat,
		StringComparison stringComparison,
		StringCaseConversion stringCaseConversion = StringCaseConversion.NoConversion,
		bool generateEnumerable = true,
		bool generateToString = true,
		bool generateComparison = true,
		bool generateDefaultConstructor = true,
		bool forbidParameterlessConstruction = true, 
		bool generateStaticDefault = false,
		string? propertyName = null,
		bool propertyIsPublic = false,
		bool valueIsNullable = false,
		bool useValidationExceptions = true)
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

#pragma warning restore IDE0060 // Remove unused parameter
