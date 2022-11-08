namespace CodeChops.DomainDrivenDesign.DomainModeling.Attributes.ValueObjects;

#pragma warning disable IDE0060 // Remove unused parameter

/// <summary>
/// Generates a value object with a single structural value of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the only structural value.</typeparam>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
public sealed class GenerateValueObjectAttribute<T> : Attribute
	where T : struct, IComparable<T>, IEquatable<T>
{
	/// <param name="minimumValue">The minimum value.</param>
	/// <param name="maximumValue">The maximum value.</param>
	/// <param name="generateToString">Generates a ToString(). Default: true.</param>
	/// <param name="generateComparison">
	/// Generates Equals(), GetHashCode(), comparison operators (and CompareTo() if possible).
	/// Disable when other properties exist.
	/// Default: true.
	/// </param>
	/// <param name="addCustomValidation">Forces to create a validate method so custom validation can be implemented. Default: true.</param>
	/// <param name="constructorIsPublic">Set to true if the default constructor should be public, if false it will be private. Default: true.</param>
	/// <param name="forbidParameterlessConstruction">If true (default), it creates an obsolete parameterless private constructor that throws an exception.</param>
	/// <param name="generateStaticDefault">Generate a static property with a default value. Default: false.</param>
	/// <param name="propertyName">The name of the property. Default: 'Value'.</param>
	/// <param name="propertyIsPublic">If true, the generated property will be publicly accessible (not settable). Default: false.</param>
	/// <param name="allowNull">Allow the underlying value to be null. Default: false.</param>
	/// <param name="useValidationExceptions">Throw validation exceptions instead of system exceptions. Default: true.</param>
	// ReSharper disable always UnusedParameter.Local
	public GenerateValueObjectAttribute(
		int minimumValue, 
		int maximumValue,
		bool generateToString = true,
		bool generateComparison = true,
		bool addCustomValidation = true,
		bool constructorIsPublic = true,
		bool forbidParameterlessConstruction = true, 
		bool generateStaticDefault = false,
		string? propertyName = null,
		bool propertyIsPublic = false,
		bool allowNull = false,
		bool useValidationExceptions = true)
	{
		// These parameters will be read from the attribute arguments itself and therefore don't need to be assigned.
	}
}

#pragma warning restore IDE0060 // Remove unused parameter
