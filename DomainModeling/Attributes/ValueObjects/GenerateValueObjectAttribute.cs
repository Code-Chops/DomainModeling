namespace CodeChops.DomainDrivenDesign.DomainModeling.Attributes.ValueObjects;

#pragma warning disable IDE0060 // Remove unused parameter

/// <summary>
/// <para>
/// Generates a value object with a single structural underlying value of type <typeparamref name="T"/>.
/// Any manually extra added property is not being used for calculating Equals or GetHashCode.
/// Value objects that have been created by the generator can also be used as underlying type.
/// </para>
/// <para>
/// Value tuples can be used if you need to use multiple underlying values than one:
/// Be sure that 'propertyIsPublic' is set to false and expose the individual parameters by exposing each value of the tuple. 
/// </para>
/// </summary>
/// <typeparam name="T">The type of the only structural value.</typeparam>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
public sealed class GenerateValueObjectAttribute<T> : Attribute
	where T : struct, IComparable<T>, IEquatable<T>
{
	/// <param name="minimumValue">The minimum value.</param>
	/// <param name="maximumValue">The maximum value.</param>
	/// <param name="generateToString">Generates a ToString(). Default: true.</param>
	/// <param name="generateComparison">Generates Equals(), GetHashCode(), comparison operators (and CompareTo() if possible). Default: true.</param>
	/// <param name="generateDefaultConstructor">Set to true if a default constructor should be generated. If it is false, no default constructor will be generated. Default: true.</param>
	/// <param name="forbidParameterlessConstruction">If true (default), it creates an obsolete parameterless private constructor that throws an exception.</param>
	/// <param name="generateStaticDefault">Generate a static property with a default value. Default: false.</param>
	/// <param name="propertyName">The name of the property. Default: 'Value'.</param>
	/// <param name="propertyIsPublic">If true, the generated property will be publicly accessible (not settable). Default: false.</param>
	/// <param name="valueIsNullable">Allow the underlying value to be null. Default: false.</param>
	/// <param name="useValidationExceptions">Throw validation exceptions instead of system exceptions. Default: true.</param>
	// ReSharper disable always UnusedParameter.Local
	public GenerateValueObjectAttribute(
		int minimumValue, 
		int maximumValue,
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

#pragma warning restore IDE0060 // Remove unused parameter
