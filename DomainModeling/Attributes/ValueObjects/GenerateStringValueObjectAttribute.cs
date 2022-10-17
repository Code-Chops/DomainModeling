namespace CodeChops.DomainDrivenDesign.DomainModeling.Attributes.ValueObjects;

#pragma warning disable IDE0060

/// <summary>
/// Generates a value object with a string as single structural value.
/// </summary>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
public sealed class GenerateStringValueObjectAttribute : Attribute
{
	/// <param name="generateToString">Generates a ToString(). Default: true.</param>
	/// <param name="generateComparison">
	/// Generates Equals(), GetHashCode(), comparison operators (and CompareTo() if possible).
	/// Disable when other properties exist.
	/// Default: true.
	/// </param>
	/// <param name="addCustomValidation">Forces to create a Validate method so custom validation can be implemented. Default: true.</param>
	/// <param name="generateDefaultConstructor">Generates a default constructor. Default: true.</param>
	/// <param name="addParameterlessConstructor">
	/// If true, it forces to create a parameterless constructor (in order to define a default value).
	/// If false, it creates an obsolete parameterless private constructor that throws an exception.
	/// Default: false.
	/// </param>
	/// <param name="generateStaticDefault">Generate a static property with a default value. Default: false.</param>
	/// <param name="generateEnumerable">Generates an IEnumerable implementation and indexer.</param>
	/// <param name="propertyName">The name of the property. Default: Value.</param>
	/// <param name="propertyIsPublic">If true, the generated property will be publicly accessible (not settable).</param>
	/// <param name="allowNull">Allow the string to be null. Default: false.</param>
	/// <param name="minimumLength">The minimum length of the string. Default: 0.</param>
	/// <param name="maximumLength">The maximum length of the string. Default: no maximum length.</param>
	/// <param name="stringCaseConversion">Converts the case to upper/lower case. Default: no conversion.</param>
	/// <param name="stringFormat">Make the string only accept certain characters. Default: no special format.</param>
	/// <param name="compareOptions">See <see cref="StringComparison"/>. Default: ordinal.</param>
	// ReSharper disable always UnusedParameter.Local
	public GenerateStringValueObjectAttribute(
		bool generateToString = true,
		bool generateComparison = true,
		bool addCustomValidation = true,
		bool generateDefaultConstructor = true,
		bool addParameterlessConstructor = false, 
		bool generateStaticDefault = false,
		bool generateEnumerable = true,
		string? propertyName = null,
		bool propertyIsPublic = false,
		bool allowNull = false,
		int minimumLength = 0,
		int maximumLength = Int32.MinValue,
		StringCaseConversion stringCaseConversion = StringCaseConversion.NoConversion,
		StringFormat stringFormat = StringFormat.Default,
		StringComparison compareOptions = StringComparison.Ordinal)
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

#pragma warning restore IDE0060