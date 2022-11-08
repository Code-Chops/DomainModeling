namespace CodeChops.DomainDrivenDesign.DomainModeling.Attributes.ValueObjects;

#pragma warning disable IDE0060 // Remove unused parameter

/// <summary>
/// Generates a value object with a string as single structural value.
/// </summary>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
public sealed class GenerateStringValueObjectAttribute : Attribute
{
	/// <param name="minimumLength">The minimum length of the string.</param>
	/// <param name="maximumLength">The maximum length of the string.</param>
	/// <param name="stringFormat">Make the string only accept certain characters.</param>
	/// <param name="stringComparison">See <see cref="StringComparison"/>.</param>
	/// <param name="generateToString">Generates a ToString(). Default: true.</param>
	/// <param name="generateComparison">
	/// Generates Equals(), GetHashCode(), comparison operators (and CompareTo() if possible).
	/// Disable when other properties exist.
	/// Default: true.
	/// </param>
	/// <param name="addCustomValidation">Forces to create a Validate method so custom validation can be implemented. Default: true.</param>
	/// <param name="constructorIsPublic">Set to true if the default constructor should be public, if false it will be private. Default: true.</param>
	/// <param name="forbidParameterlessConstruction">If true (default), it creates an obsolete parameterless private constructor that throws an exception.</param>
	/// <param name="generateStaticDefault">Generate a static property with a default value. Default: false.</param>
	/// <param name="generateEnumerable">Generates an IEnumerable implementation and indexer. Default: true.</param>
	/// <param name="propertyName">The name of the property. Default: 'Value'.</param>
	/// <param name="propertyIsPublic">If true, the generated property will be publicly accessible (not settable). Default: false.</param>
	/// <param name="allowNull">Allow the string to be null. Default: false.</param>
	/// <param name="stringCaseConversion">Converts the case to upper/lower case. Default: no conversion.</param>
	/// <param name="useValidationExceptions">Throw validation exceptions instead of system exceptions. Default: true.</param>
	// ReSharper disable always UnusedParameter.Local
	public GenerateStringValueObjectAttribute(
		int minimumLength,
		int maximumLength,
		StringFormat stringFormat,
		StringComparison stringComparison,
		bool generateToString = true,
		bool generateComparison = true,
		bool addCustomValidation = true,
		bool constructorIsPublic = true,
		bool forbidParameterlessConstruction = true, 
		bool generateStaticDefault = false,
		bool generateEnumerable = true,
		string? propertyName = null,
		bool propertyIsPublic = false,
		bool allowNull = false,
		StringCaseConversion stringCaseConversion = StringCaseConversion.NoConversion,
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
