namespace CodeChops.DomainDrivenDesign.DomainModeling.Attributes.ValueObjects;

#pragma warning disable IDE0060

/// <summary>
/// Generates a value object with a single structural value of type string.
/// </summary>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
public sealed class GenerateStringValueObject : Attribute
{
	/// <param name="generateToString">Generates a ToString(). Default: true.</param>
	/// <param name="generateComparison">
	/// Generates Equals(), GetHashCode(), comparison operators (and CompareTo() if possible).
	/// Disable when other properties exist.
	/// Default: true.
	/// </param>
	/// <param name="addCustomValidation">Forces to create a Validate method so custom validation can be implemented. Default: true.</param>
	/// <param name="generateDefaultConstructor">Generates a default constructor. Default: true.</param>
	/// <param name="generateParameterlessConstructor">
	/// Generates a parameterless constructor that assigns a default value to the property.
	/// If false, it creates an obsolete parameterless private constructor that throws an exception.
	/// Default: false.
	/// </param>
	/// <param name="generateEmptyStatic">Generate a static property with a default value. Default: false.</param>
	/// <param name="propertyName">The name of the property. Default: Value.</param>
	/// <param name="allowNull">Allow the string to be null. Default: false.</param>
	/// <param name="minimumLength">The minimum length of the string. Default: no minimum length.</param>
	/// <param name="maximumLength">The maximum length of the string. Default: no maximum length.</param>
	/// <param name="stringCaseConversion">Converts the case to upper/lower case. Default: no conversion.</param>
	/// <param name="stringFormat">Make the string only accept certain characters. Default: no special format.</param>
	/// <param name="compareOptions">See <see cref="StringComparison"/>. Default: ordinal.</param>
	// ReSharper disable always UnusedParameter.Local
	public GenerateStringValueObject(
		bool generateToString = true,
		bool generateComparison = true,
		bool addCustomValidation = true,
		bool generateDefaultConstructor = true,
		bool generateParameterlessConstructor = false, 
		bool generateEmptyStatic = false,
		string? propertyName = null,
		bool allowNull = false,
		int minimumLength = Int32.MinValue,
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